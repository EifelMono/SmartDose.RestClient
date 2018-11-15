using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.WcfClient.Services
{
    public enum FlowState
    {
        Init,
        Running,
        Error,
        AssemblyError,
    }

    public class CommunicationServiceCore : IDisposable
    {
        public CommunicationServiceCore(string endpointAddress, SecurityMode securityMode = SecurityMode.None)
        {
            EndpointAddress = endpointAddress;
            SecurityMode = securityMode;
        }

        #region Assembly Client
        protected Dictionary<string, MethodInfo> ServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        protected Dictionary<string, MethodInfo> AsyncServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        protected Dictionary<string, MethodInfo> EventAddMethods { get; set; } = new Dictionary<string, MethodInfo>();

        // SubscribeForCallbacksAsync
        protected MethodInfo SubscribeForCallBacksMethod { get; set; } = null;
        // UnsubscribeForCallbacksAsync
        protected MethodInfo UnsubscribeForCallbacksMethod { get; set; } = null;

        public string AssemblyFilename { get; private set; }

        public bool IsAssembly { get => !string.IsNullOrEmpty(AssemblyFilename); }

        protected AppDomain AppDomain { get; set; }
        protected AssemblyName AssemblyName { get; set; }

        protected Assembly Assembly { get; set; }

        string ConnectionStringToConnectionName(string connectionstring)
        {
            var result = connectionstring.Split(new[] { "//" }, StringSplitOptions.None)[1].Replace(":", "_").Replace("/", "_");
            if (result.EndsWith("_"))
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        public string ConnectionString { get; set; }
        public string ConnectionName { get; set; }

        public string ConnectionDirectory { get; set; }
        public string ConnectionAssemblyFileName { get; set; }

        public CommunicationServiceCore(string assemblyFilename, string endpointAddress, SecurityMode securityMode= SecurityMode.None)
                    : this(endpointAddress, securityMode)
        {
            AssemblyFilename = assemblyFilename;
        }

        protected bool LoadAssembly()
        {
            if (!IsAssembly)
                return true;
            try
            {
                AppDomain = AppDomain.CreateDomain(Path.GetFileNameWithoutExtension(AssemblyFilename));
                AssemblyName = new AssemblyName() { CodeBase = Path.GetDirectoryName(AssemblyFilename) };
                Assembly = AppDomain.Load(AssemblyFilename);
                var types = Assembly.GetTypes();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected bool UnloadAssembly()
        {
            if (!IsAssembly)
                return true;
            try
            {
                AppDomain.Unload(AppDomain);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Service 

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

        #region Run
        protected bool CancelRequested { get; set; }
        public bool IsCancelRequested { get => CancelRequested; }

        public void Cancel()
        {
            CancelRequested = true;
        }


        public FlowState FlowState { get; protected set; }
        public void Start()
        {
            CancelRequested = false;
            Task.Run(async () =>
            {
                try
                {
                    FlowState = FlowState.Init;
                    if (!LoadAssembly())
                    {
                        FlowState = FlowState.AssemblyError;
                        return;
                    }
                    while (!IsCancelRequested)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(500));
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    UnloadAssembly();
                }
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
        {
            if (SubscribeForCallBacksMethod != null)
                try
                {
                    SubscribeForCallBacksMethod.Invoke(Client, new object[] { });
                }
                catch (Exception ex)
                {

                }
        }

        protected virtual void UnsubscribeCallBacks()
        {
            {
                try
                {
                    UnsubscribeForCallbacksMethod.Invoke(Client, new object[] { });
                }
                catch
                {

                }
            }
        }
    }

    public class CommunicationService: CommunicationServiceCore
    {
        public CommunicationService(string connectionString, string endpointAddress, SecurityMode securityMode = SecurityMode.None) : base(connectionString, endpointAddress, securityMode) { }

    }
    public class CommunicationService<TClient> : CommunicationServiceCore where TClient : ICommunicationObject, new()
    {
        public CommunicationService(string endpointAddress, SecurityMode securityMode= SecurityMode.None) : base(endpointAddress, securityMode)
        {
            ClientType = typeof(TClient);
        }

        public new TClient Client { get => (TClient)base.Client; protected set => Client = value; }

        protected override void NewClient() => base.NewClient();
    }
}
