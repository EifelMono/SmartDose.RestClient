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
            => ServiceNotiyEvent(Services.ServiceNotifyEvent.ClientClosing);
        private void Client_Closed(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotifyEvent.ClientClosed);
        private void Client_Faulted(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotifyEvent.ClientFaulted);
        private void Client_Opened(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotifyEvent.ClientOpened);
        private void Client_Opening(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotifyEvent.ClientOpening);
        #endregion

        #region Run
        protected TaskCompletionSource<ServiceNotifyEvent> ServiceNotiyfierSource
                    = new TaskCompletionSource<ServiceNotifyEvent>();

        protected void ServiceNotiyEvent(ServiceNotifyEvent serviceNotifyEvent)
        {
            ServiceNotiyfierSource.SetResult(serviceNotifyEvent);
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
                        switch (await ServiceNotiyfierSource.Task)
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
                            switch (await ServiceNotiyfierSource.Task)
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
            ServiceNotiyEvent(Services.ServiceNotifyEvent.ServiceStart);
        }
        public void Stop()
        {
            ServiceNotiyEvent(Services.ServiceNotifyEvent.ServiceStop);
        }
        #endregion

        public void Dispose()
        {
            ServiceNotiyEvent(Services.ServiceNotifyEvent.ServiceDispose);
        }

        protected virtual void SubscribeCallBacks()
            => Catcher(() => SubscribeForCallBacksMethod?.Invoke(Client, new object[] { }));

        protected virtual void UnsubscribeCallBacks()
            => Catcher(() => UnsubscribeForCallbacksMethod?.Invoke(Client, new object[] { }));
    }
}
