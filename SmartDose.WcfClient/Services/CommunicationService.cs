using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace SmartDose.WcfClient.Services
{
    public class CommunicationService : CommunicationServiceCore
    {
        public CommunicationService(WcfItem wcfItem, string endpointAddress, SecurityMode securityMode = SecurityMode.None)
            : base(wcfItem, endpointAddress, securityMode) { }
        public new List<WcfMethod> WcfMethodes { get => base.WcfMethods; set => base.WcfMethods = value; }

        public new void SubscribeCallBacks()
            => base.SubscribeCallBacks();

        public new void UnsubscribeCallBacks()
            => base.UnsubscribeCallBacks();
    }

    public class CommunicationService<TClient> : CommunicationServiceCore where TClient : ICommunicationObject, new()
    {
        public CommunicationService(string endpointAddress, SecurityMode securityMode = SecurityMode.None) : base(endpointAddress, securityMode)
        {
            ClientType = typeof(TClient);
        }

        public new TClient Client { get => (TClient)base.Client; protected set => Client = value; }

        protected override void NewClient() => base.NewClient();

        public new void SubscribeCallBacks()
            => base.SubscribeCallBacks();

        public new void UnsubscribeCallBacks()
            => base.UnsubscribeCallBacks();
    }
}
