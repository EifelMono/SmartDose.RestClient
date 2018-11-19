using System;
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
        protected List<WcfMethode> WcfMethodes { get; set; } = new List<WcfMethode>();

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
        protected Dictionary<string, MethodInfo> ServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        protected Dictionary<string, MethodInfo> AsyncServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        protected Dictionary<string, MethodInfo> EventAddMethods { get; set; } = new Dictionary<string, MethodInfo>();

        // SubscribeForCallbacksAsync
        protected MethodInfo SubscribeForCallBacksMethod { get; set; } = null;
        // UnsubscribeForCallbacksAsync
        protected MethodInfo UnsubscribeForCallbacksMethod { get; set; } = null;

        protected virtual void SubscribeCallBacks()
            => Catcher(() => SubscribeForCallBacksMethod?.Invoke(Client, new object[] { }));

        protected virtual void UnsubscribeCallBacks()
            => Catcher(() => UnsubscribeForCallbacksMethod?.Invoke(Client, new object[] { }));

        protected bool FindAssemblyThings(Assembly assembly)
        {
            try
            {
                ClientType = assembly.GetTypes()
                        .Where(t => t.GetInterfaces().Where(i => i == typeof(ICommunicationObject)).Any())
                        .Where(t => t.FullName.EndsWith("Client")).FirstOrDefault();
                foreach (var m in ClientType.GetMethods())
                {
                    ServiceMethods[m.Name] = m;
                    if (m.Name.EndsWith("Async"))
                        AsyncServiceMethods[m.Name] = m;
                    if (m.Name.StartsWith("add"))
                        EventAddMethods[m.Name] = m;
                    if (m.Name.EndsWith("SubscribeForCallbacksAsync"))
                        SubscribeForCallBacksMethod = m;
                    if (m.Name.EndsWith("UnsubscribeForCallbacksAsync"))
                        UnsubscribeForCallbacksMethod = m;
                }
                return true;
            }
            catch(Exception ex)
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
                OnServiceNotifyEvent?.Invoke(this, new ServiceNotifyEventArgs { Value = serviceNotifyEvent });
            }
            catch { }
        }
        #endregion

        #region Inner Events
        protected ICommunicationObject SetClientServiceNotifyEvents(ICommunicationObject client, bool on)
        {
            switch (on)
            {
                case true:
                    client.Opening += Client_Opening;
                    client.Opened += Client_Opened;
                    client.Faulted += Client_Faulted;
                    client.Closing += Client_Closing;
                    client.Closed += Client_Closed;
                    break;
                case false:
                    client.Opening -= Client_Opening;
                    client.Opened -= Client_Opened;
                    client.Faulted -= Client_Faulted;
                    client.Closing -= Client_Closing;
                    client.Closed -= Client_Closed;
                    break;
            }
            return client;
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

                        #region wait for Start or Dispose
                        bool preRunning = true;
                        while (preRunning)
                        {
                            ClientServiceNotifyInit();
                            switch (await ClientServiceNotifyComletionSource.Task)
                            {
                                case Services.ServiceNotifyEvent.ServiceStart:
                                    preRunning = false;
                                    break;
                                case Services.ServiceNotifyEvent.ServiceDispose:
                                    return;
                            }
                        }
                        #endregion

                        ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceRunning);

                        while (mainRunning)
                        {
                            ClientServiceNotifyInit();
                            switch (await ClientServiceNotifyComletionSource.Task)
                            {
                                case Services.ServiceNotifyEvent.ServiceStart:
                                    break;
                                case Services.ServiceNotifyEvent.ServiceStop:
                                    break;
                                case Services.ServiceNotifyEvent.ServiceDispose:
                                    mainRunning = false;
                                    break;
                                case Services.ServiceNotifyEvent.ClientOpening:
                                    break;
                                case Services.ServiceNotifyEvent.ClientOpened:
                                    break;
                                case Services.ServiceNotifyEvent.ClientFaulted:
                                    break;
                                case Services.ServiceNotifyEvent.ClientClosing:
                                    break;
                                case Services.ServiceNotifyEvent.ClientClosed:
                                    break;
                            }
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
