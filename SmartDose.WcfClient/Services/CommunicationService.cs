using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static SmartDose.Core.SafeExecuter;

namespace SmartDose.WcfClient.Services
{
    public enum ServiceNotiyEvent
    {
        ClientClosing,
        ClientClosed,
        ClientFaulted,
        ClientOpening,
        ClientOpened,

        AppCanel
    }

    public class CommunicationServiceCore : IDisposable
    {
        public CommunicationServiceCore(string endpointAddress, SecurityMode securityMode = SecurityMode.None)
        {
            EndpointAddress = endpointAddress;
            SecurityMode = securityMode;
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
            SetClientNotifyEvents(Client, true);
        }
        #endregion

        #region Events
        protected ICommunicationObject SetClientNotifyEvents(ICommunicationObject client, bool on)
        {
            switch (on)
            {
                case true:
                    client.Closing += Client_Closing;
                    client.Closed += Client_Closed;
                    client.Faulted += Client_Faulted;
                    client.Opening += Client_Opening;
                    client.Opened += Client_Opened;
                    break;
                case false:
                    client.Closing -= Client_Closing;
                    client.Closed -= Client_Closed;
                    client.Faulted -= Client_Faulted;
                    client.Opening -= Client_Opening;
                    client.Opened -= Client_Opened;
                    break;
            }
            return client;
        }

        private void Client_Closing(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotiyEvent.ClientClosing);
        private void Client_Closed(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotiyEvent.ClientClosed);
        private void Client_Faulted(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotiyEvent.ClientFaulted);
        private void Client_Opened(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotiyEvent.ClientOpened);
        private void Client_Opening(object sender, EventArgs e)
            => ServiceNotiyEvent(Services.ServiceNotiyEvent.ClientOpening);
        #endregion

        #region Run
        protected bool CancelRequested { get; set; }
        public bool IsCancelRequested { get => CancelRequested; }

        public void Cancel()
        {
            CancelRequested = true;
        }

        protected void ServiceNotiyEvent(ServiceNotiyEvent serviceNotifyEvent)
        {

        }

        public void Start()
        {
            CancelRequested = false;
            Task.Run(async () =>
            {
                await Task.Delay(1);



                // remove
                // SetClientNotifyEvents(Client, false);
            });
        }

        public void Stop()
        {
            try
            {
                Cancel();
                if (Client != null)
                {
                    Client.Abort();
                }
                Client = null;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        public void Dispose()
        {
            Stop();
        }

        protected virtual void SubscribeCallBacks()
            => Catcher(() => SubscribeForCallBacksMethod?.Invoke(Client, new object[] { }));

        protected virtual void UnsubscribeCallBacks()
            => Catcher(() => UnsubscribeForCallbacksMethod?.Invoke(Client, new object[] { }));
    }

    public class CommunicationService : CommunicationServiceCore
    {
        public CommunicationService(WcfItem wcfItem, string endpointAddress, SecurityMode securityMode = SecurityMode.None)
            : base(wcfItem, endpointAddress, securityMode) { }

    }
    public class CommunicationService<TClient> : CommunicationServiceCore where TClient : ICommunicationObject, new()
    {
        public CommunicationService(string endpointAddress, SecurityMode securityMode = SecurityMode.None) : base(endpointAddress, securityMode)
        {
            ClientType = typeof(TClient);
        }

        public new TClient Client { get => (TClient)base.Client; protected set => Client = value; }

        protected override void NewClient() => base.NewClient();
    }
}
