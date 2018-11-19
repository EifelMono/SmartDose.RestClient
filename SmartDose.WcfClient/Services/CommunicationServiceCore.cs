using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
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

        #region Events
        protected ICommunicationObject SetClientNotifyEvents(ICommunicationObject client, bool on)
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
            => ServiceNotifyEvent(Services.ServiceNotifyEvent.ClientClosing);
        private void Client_Closed(object sender, EventArgs e)
            => ServiceNotifyEvent(Services.ServiceNotifyEvent.ClientClosed);
        private void Client_Faulted(object sender, EventArgs e)
            => ServiceNotifyEvent(Services.ServiceNotifyEvent.ClientFaulted);
        private void Client_Opened(object sender, EventArgs e)
            => ServiceNotifyEvent(Services.ServiceNotifyEvent.ClientOpened);
        private void Client_Opening(object sender, EventArgs e)
            => ServiceNotifyEvent(Services.ServiceNotifyEvent.ClientOpening);
        #endregion

        #region Run
        protected object ServiceNotifyComlitionSourceLock = new object();
        protected TaskCompletionSource<ServiceNotifyEvent> ServiceNotifyComletionSource
                    = new TaskCompletionSource<ServiceNotifyEvent>();

        protected void ServiceNotifyInit()
        {
            lock (ServiceNotifyComlitionSourceLock)
                ServiceNotifyComletionSource = new TaskCompletionSource<ServiceNotifyEvent>();
        }

        protected void ServiceNotifyEvent(ServiceNotifyEvent serviceNotifyEvent)
        {
            bool send = false;

            for (var i = 0; i < 10; i++)
            {
                lock (ServiceNotifyComlitionSourceLock)
                    send = ServiceNotifyComletionSource.TrySetResult(serviceNotifyEvent);
                if (send)
                    break;
            }

            if (!send)
            {
                // gute frage, nächst frage 
            }
        }

        protected virtual void Run()
        {
            Task.Run(async () =>
            {
                try
                {
                    #region wait for Start or Dispose
                    bool running = true;
                    while (running)
                    {
                        ServiceNotifyInit();
                        switch (await ServiceNotifyComletionSource.Task)
                        {
                            case Services.ServiceNotifyEvent.ServiceStart:
                                running = false;
                                break;
                            case Services.ServiceNotifyEvent.ServiceDispose:
                                return;
                        }
                    }
                    #endregion

                    #region Client Run
                    running = true;
                    using (var communicationAssembly = new CommunicationAssembly(AssemblyFilename))
                    {
                        while (running)
                        {
                            ServiceNotifyInit();
                            switch (await ServiceNotifyComletionSource.Task)
                            {
                                case Services.ServiceNotifyEvent.ServiceStart:
                                    running = false;
                                    break;
                                case Services.ServiceNotifyEvent.ServiceStop:
                                    running = false;
                                    break;
                                case Services.ServiceNotifyEvent.ServiceDispose:
                                    running = false;
                                    break;
                                case Services.ServiceNotifyEvent.ClientOpening:
                                    running = false;
                                    break;
                                case Services.ServiceNotifyEvent.ClientOpened:
                                    running = false;
                                    break;
                                case Services.ServiceNotifyEvent.ClientFaulted:
                                    running = false;
                                    break;
                                case Services.ServiceNotifyEvent.ClientClosing:
                                    running = false;
                                    break;
                                case Services.ServiceNotifyEvent.ClientClosed:
                                    running = false;
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
            ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceStart);
        }
        public void Stop()
        {
            ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceStop);
        }
        #endregion

        bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                ServiceNotifyEvent(Services.ServiceNotifyEvent.ServiceDispose);
            }
            disposed = true;
        }
         
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void SubscribeCallBacks()
            => Catcher(() => SubscribeForCallBacksMethod?.Invoke(Client, new object[] { }));

        protected virtual void UnsubscribeCallBacks()
            => Catcher(() => UnsubscribeForCallbacksMethod?.Invoke(Client, new object[] { }));
    }
}
