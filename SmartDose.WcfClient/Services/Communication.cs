using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using static SmartDose.Core.SafeExecuter;

namespace SmartDose.WcfClient.Services
{
    public class Communication : IDisposable
    {
        #region Properties
        public Type FindAssemblyType { get; set; }
        public string EndPointAddress { get; set; }


        public Assembly ServiceAssembly { get; set; }
        public Type ServiceClientType { get; set; }
        public ICommunicationObject ServiceClient { get; set; }


        public Dictionary<string, MethodInfo> ServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        public Dictionary<string, MethodInfo> AsyncServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        public Dictionary<string, MethodInfo> EventAddMethods { get; set; } = new Dictionary<string, MethodInfo>();

        // SubscribeForCallbacksAsync
        public MethodInfo SubscribeForCallBacksMethod { get; set; } = null;
        // UnsubscribeForCallbacksAsync
        public MethodInfo UnsubscribeForCallbacksMethod { get; set; } = null;

        #endregion

        public Communication(Type findAssemblyType, string endPointAddress)
        {
            FindAssemblyType = findAssemblyType;
            EndPointAddress = endPointAddress;

            ServiceAssembly = findAssemblyType.Assembly;
            ServiceClientType = FindServiceClient();
            FindServiceClientMethods();

            NewServiceClient();
        }

        ~Communication()
        {
            if (ServiceClient != null)
                ServiceClient.Close();
            UninstallEvents();
        }

        protected Type FindServiceClient()
            => ServiceAssembly.GetTypes()
                .Where(t => t.GetInterfaces().Where(i => i == typeof(ICommunicationObject)).Any())
                .Where(t => t.FullName.EndsWith("Client")).FirstOrDefault();

        protected void FindServiceClientMethods()
        {
            foreach (var m in ServiceClientType.GetMethods())
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
        }

        protected ICommunicationObject CreateServiceClient(string endPointAddress)
            => ServiceClient = (ICommunicationObject)Activator.CreateInstance(ServiceClientType,
                    new NetTcpBinding(SecurityMode.None),
                    new EndpointAddress(endPointAddress));

        public void NewServiceClient()
        {
            if (ServiceClient != null)
            {
                UninstallEvents();
                try
                {
                    ServiceClient.Close();
                }
                catch { }
                try
                {
                    var d = ServiceClient as IDisposable;
                    if (d != null)
                        d.Dispose();
                }
                catch { }

            }
            try
            {
                ServiceClient = CreateServiceClient(EndPointAddress);
                InstallEvents();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #region Methods
        protected object[] GetMethodsParamsObjects(string name)
        {
            var objects = new List<object>();
            try
            {
                foreach (var p in ServiceMethods[name].GetParameters())
                    objects.Add(p.ParameterType.IsArray ? Activator.CreateInstance(p.ParameterType, 0) : Activator.CreateInstance(p.ParameterType));
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return objects.ToArray(); ;
        }

        public async Task<(bool Ok, object Value)> CallMethodAsync(string name, object[] paramValues)
        {
            var m = ServiceMethods[name];
            try
            {
                #region Catch Errors befor the could happen on the OTHER SIDE
                for (var i = 0; i < paramValues.Count(); i++)
                {
                    var p = paramValues[i];
                    if (p != null)
                    {
                        if (p.GetType().FullName.EndsWith(".PageFilter"))
                        {
                            if (p.GetType().GetProperty("PageSize").GetValue(p) is uint ps && ps == 0)
                                paramValues[i] = null;
                        }
                        else
                        if (p.GetType().FullName.EndsWith(".SortFilter"))
                        {
                            var v = p.GetType().GetProperty("AttributeName").GetValue(p);
                            if (v is null || v is string sn && string.IsNullOrEmpty(sn))
                                paramValues[i] = null;
                        }
                    }
                }
                #endregion
                return (true, await (dynamic)m.Invoke(ServiceClient, paramValues));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return (false, null);
            }
        }

        public async Task<(bool Ok, object Value)> CallMethodAsParamsAync(string name, params object[] paramValues)
            => await CallMethodAsync(name, paramValues).ConfigureAwait(false);

        #endregion

        #region Events

        public Action<object, object> OnEvents { get; set; }

        protected void Events(object sender, object args)
            => Catcher(() => OnEvents?.Invoke(sender, args));

        private MethodInfo _EventsMethodInfo = null;
        protected MethodInfo EventsMethodInfo
        {
            get => _EventsMethodInfo
                ?? (_EventsMethodInfo = GetType().GetMethod("Events", BindingFlags.NonPublic | BindingFlags.Instance));
        }

        public void InstallEvents()
        {
            foreach (var e in ServiceClient.GetType().GetEvents())
                Catcher(() => e.AddEventHandler(ServiceClient,
                                Delegate.CreateDelegate(e.EventHandlerType, this, EventsMethodInfo)));
        }

        public void UninstallEvents()
        {
            foreach (var e in ServiceClient.GetType().GetEvents())
                Catcher(() => e.RemoveEventHandler(ServiceClient,
                                Delegate.CreateDelegate(e.EventHandlerType, this, EventsMethodInfo)));
        }
        #endregion

        public void Dispose()
        {
            try
            {
                UninstallEvents();
                ServiceClient.Close();
            }
            catch { }
        }
    }
}
