﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using SmartDose.Core.Extensions;
using static SmartDose.Core.SafeExecuter;

namespace SmartDose.WcfClient.Services
{
    public class CommunicationServiceCore : IDisposable
    {
        protected List<WcfMethod> WcfMethods { get; set; } = new List<WcfMethod>();

        public CommunicationServiceCore(string endpointAddress, SecurityMode securityMode = SecurityMode.None)
        {
            EndpointAddress = endpointAddress;
            SecurityMode = securityMode;
            Run();
        }

        protected WcfItem WcfItem { get; set; }
        protected string AssemblyFilename { get; private set; }
        protected bool IsAssembly { get => !string.IsNullOrEmpty(AssemblyFilename); }

        public CommunicationServiceCore(WcfItem wcfItem, string endpointAddress, SecurityMode securityMode = SecurityMode.None)
            : this(endpointAddress, securityMode)
        {
            WcfItem = wcfItem;
            EndpointAddress = wcfItem.ConnectionStringUse;
            AssemblyFilename = WcfClientGlobals.WcfItemToAssemblyFilename(wcfItem);
        }

        #region Assembly Client
        protected Dictionary<string, MethodInfo> AsyncServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        protected Dictionary<string, MethodInfo> EventAddMethods { get; set; } = new Dictionary<string, MethodInfo>();

        // Methods by name
        protected MethodInfo SubscribeForCallBacksAsyncMethod { get; set; } = null;
        protected MethodInfo UnsubscribeForCallbacksAsyncMethod { get; set; } = null;

        protected MethodInfo OpenAsyncMethod { get; set; } = null;
        protected MethodInfo CloseAsyncMethod { get; set; } = null;

        protected virtual void SubscribeCallBacksAsync()
            => Catcher(() => SubscribeForCallBacksAsyncMethod?.Invoke(Client, new object[] { }));

        protected virtual void UnsubscribeCallBacksAsync()
            => Catcher(() => UnsubscribeForCallbacksAsyncMethod?.Invoke(Client, new object[] { }));

        protected virtual void OpenAsync()
            => Catcher(() => OpenAsyncMethod?.Invoke(Client, new object[] { }));

        protected virtual void CloseAsync()
            => Catcher(() => CloseAsyncMethod?.Invoke(Client, new object[] { }));

        protected bool FindAssemblyThings(Assembly assembly)
        {
            try
            {
                ClientType = assembly.GetTypes()
                        .Where(t => t.GetInterfaces().Where(i => i == typeof(ICommunicationObject)).Any())
                        .Where(t => t.FullName.EndsWith("Client")).FirstOrDefault();
                WcfMethods.Clear();
                foreach (var m in ClientType.GetMethods())
                {
                    if (m.Name.EndsWith("Async"))
                    {
                        if (m.Name.EndsWith("CallbacksAsync"))
                        {
                            if (m.Name.EndsWith("SubscribeForCallbacksAsync"))
                                SubscribeForCallBacksAsyncMethod = m;
                            if (m.Name.EndsWith("UnsubscribeForCallbacksAsync"))
                                UnsubscribeForCallbacksAsyncMethod = m;
                        }
                        else
                        {
                            if (m.Name == "OpenAsync")
                                OpenAsyncMethod = m;
                            else
                            if (m.Name == "CloseAsync")
                                CloseAsyncMethod = m;
                            else
                            {
                                WcfMethod wcfMethod;
                                WcfMethods.Add(wcfMethod = new WcfMethod
                                {
                                    Name = m.Name,
                                    Method = m,
                                    Output = null,
                                    Input = null
                                });
                                wcfMethod.CreateInput();
                            }
                        }
                    }
                    if (m.Name.StartsWith("add"))
                        EventAddMethods[m.Name] = m;

                }
                return true;
            }
            catch (Exception ex)
            {
                ex.LogException();
                return false;
            }
        }
        #endregion

        #region Client 

        public SecurityMode SecurityMode { get; protected set; }
        public string EndpointAddress { get; protected set; }
        public ICommunicationObject Client { get; protected set; }

        protected Type ClientType { get; set; }

        protected virtual void NewClient()
        {
            Client = (ICommunicationObject)Activator.CreateInstance(ClientType,
                   new NetTcpBinding(SecurityMode.None),
                   new EndpointAddress(EndpointAddress));
        }
        #endregion

        #region Outer Events
        public delegate void ServiceNotifyEventDelegate(object sender, ServiceNotifyEventArgs e);
        public event ServiceNotifyEventDelegate OnServiceNotifyEvent;
        protected void ServiceNotifyEvent(ServiceNotifyEvent serviceNotifyEvent)
        {
            try
            {
                Task.Run(() =>
                {
                    try
                    {
                        OnServiceNotifyEvent?.Invoke(this, new ServiceNotifyEventArgs { Value = serviceNotifyEvent });
                    }
                    catch { }
                });
            }
            catch { }
        }
        #endregion

        #region Inner Events
        protected void SetClientServiceNotifyEvents(bool on)
        {
            if (Client is null)
                return;
            switch (on)
            {
                case true:
                    Client.Opening += Client_Opening;
                    Client.Opened += Client_Opened;
                    Client.Faulted += Client_Faulted;
                    Client.Closing += Client_Closing;
                    Client.Closed += Client_Closed;
                    break;
                case false:
                    Client.Opening -= Client_Opening;
                    Client.Opened -= Client_Opened;
                    Client.Faulted -= Client_Faulted;
                    Client.Closing -= Client_Closing;
                    Client.Closed -= Client_Closed;
                    break;
            }
        }

        private void Client_Closing(object sender, EventArgs e)
            => ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ClientClosing);
        private void Client_Closed(object sender, EventArgs e)
            => ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ClientClosed);
        private void Client_Faulted(object sender, EventArgs e)
            => ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ClientFaulted);
        private void Client_Opened(object sender, EventArgs e)
            => ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ClientOpened);
        private void Client_Opening(object sender, EventArgs e)
            => ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ClientOpening);

        protected object ClientServiceNotifyComlitionSourceLock = new object();
        protected TaskCompletionSource<ServiceNotifyEvent> ClientServiceNotifyComletionSource
                    = new TaskCompletionSource<ServiceNotifyEvent>();

        protected void ClientServiceNotifyInit()
        {
            lock (ClientServiceNotifyComlitionSourceLock)
                ClientServiceNotifyComletionSource = new TaskCompletionSource<ServiceNotifyEvent>();
        }

        protected void ClientServiceNotifyEvent(ServiceNotifyEvent serviceNotifyEvent)
        {
            bool send = false;

            for (var i = 0; i < 10; i++)
            {
                lock (ClientServiceNotifyComlitionSourceLock)
                    send = ClientServiceNotifyComletionSource.TrySetResult(serviceNotifyEvent);
                if (send)
                    break;
            }

            if (!send)
            {
                // gute frage, nächst frage 
            }
        }
        #endregion

        #region Run
        protected virtual void Run()
        {
            Task.Run(async () =>
            {
                try
                {
                    #region Client Run
                    var mainRunning = true;
                    using (var communicationAssembly = new CommunicationAssembly(AssemblyFilename))
                    {
                        if (communicationAssembly.HasFileNameToLoad)
                        {
                            if (!communicationAssembly.IsLoaded)
                            {
                                ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceErrorAssemblyNotLoaded);
                                return;
                            }
                            if (!FindAssemblyThings(communicationAssembly.Assembly))
                            {
                                ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceErrorAssemblyBad);
                                return;
                            }
                        }

                        ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceInited);

                        #region wait for Start or Dispose
                        bool startWaiting = true;
                        while (startWaiting)
                        {
                            ClientServiceNotifyInit();
                            switch (await ClientServiceNotifyComletionSource.Task)
                            {
                                case Services.ServiceNotifyEvent.ServiceStart:
                                    NewClient();
                                    SetClientServiceNotifyEvents(true);
                                    OpenAsync();
                                    SubscribeCallBacksAsync();
                                    startWaiting = false;
                                    break;
                                case Services.ServiceNotifyEvent.ServiceStop:
                                case Services.ServiceNotifyEvent.ServiceDispose:
                                    return;
                            }
                        }
                        #endregion

                        try
                        {
                            bool InFault = false;
                            ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceRunning);
                            while (mainRunning)
                            {
                                ClientServiceNotifyInit();
                                switch (await ClientServiceNotifyComletionSource.Task)
                                {
                                    case Services.ServiceNotifyEvent.ServiceStart:
                                        break;
                                    case Services.ServiceNotifyEvent.ServiceStop:
                                    case Services.ServiceNotifyEvent.ServiceDispose:
                                        UnsubscribeCallBacksAsync();
                                        SetClientServiceNotifyEvents(false);
                                        CloseAsync();
                                        mainRunning = false;
                                        break;
                                    case Services.ServiceNotifyEvent.ClientOpening:
                                        break;
                                    case Services.ServiceNotifyEvent.ClientOpened:
                                        SubscribeCallBacksAsync();
                                        ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceRunning);
                                        break;
                                    case Services.ServiceNotifyEvent.ClientFaulted:
                                        if (!InFault)
                                        {
                                            ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceErrorNotConnected);
                                            InFault = true;
                                            SetClientServiceNotifyEvents(false);
                                            CloseAsync();
                                            NewClient();
                                            SetClientServiceNotifyEvents(true);
                                            OpenAsync();
                                            SubscribeCallBacksAsync();
                                            InFault = false;
                                            await Task.Delay(100);
                                        }
                                        break;
                                    case Services.ServiceNotifyEvent.ClientClosing:
                                        break;
                                    case Services.ServiceNotifyEvent.ClientClosed:
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.LogException();
                        }
                        finally
                        {

                        }

                    }
                    #endregion
                }
                catch { }
            });
        }
        public void Start()
        {
            ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceStart);
        }
        public void Stop()
        {
            ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceStop);
        }
        #endregion

        bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                ClientServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceDispose);
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
