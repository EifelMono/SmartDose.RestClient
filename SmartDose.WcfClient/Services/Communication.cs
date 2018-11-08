using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SmartDose.WcfClient.Services
{
    public class Communication
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


        #endregion

        public Communication(Type findAssemblyType, string endPointAddress)
        {
            FindAssemblyType = findAssemblyType;
            EndPointAddress = endPointAddress;

            ServiceAssembly = findAssemblyType.Assembly;
            ServiceClientType = FindServiceClient();
            ServiceClient = CreateServiceClient(endPointAddress);

            FindServiceClientMethods();
            InstallEvents();
        }

        ~Communication()
        {
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
            }
        }

        protected ICommunicationObject CreateServiceClient(string endPointAddress)
            => ServiceClient = (ICommunicationObject)Activator.CreateInstance(ServiceClientType,
                    new NetTcpBinding(SecurityMode.None),
                    new EndpointAddress(endPointAddress));

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
                            if (p.GetType().GetProperty("AttributeName").GetValue(p) is string sn && string.IsNullOrEmpty(sn))
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

        public object[] GetMethodsParamsObjects(string name)
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

        public event System.EventHandler<object> TestEvent;
        public void Test()
        {
            if (TestEvent != null)
            {
                TestEvent(null, null);
            }
        }

        public delegate void ActionDelegate(object sender, object args);
        public ActionDelegate OnEvents { get; set; }

        protected void Events(object sender, object args)
        {
            OnEvents?.Invoke(sender, args);
        }

        public event EventHandler SomeEvent;

        protected void InstallEvents()
        {
            var methodInfo1 = this.GetType().GetMethod("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventInfo = this.GetType().GetEvent("TestEvent");

            Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo1);
            eventInfo.AddEventHandler(this, handler);

            this.Test();

            foreach (var e in ServiceClient.GetType().GetEvents())
            {
                try
                {
                    Delegate handler1 = Delegate.CreateDelegate(e.EventHandlerType, this, methodInfo1);
                    e.AddEventHandler(ServiceClient, handler1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            /*
            var eventsMethod = GetType().GetMethod("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var e in ServiceClient.GetType().GetEvents())
            {
                try
                {
                    var handler = Delegate.CreateDelegate(e.EventHandlerType, null, eventsMethod);
                    e.AddEventHandler(this, handler);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            */

            //try
            //{
            //    var eventsMethod = GetType().GetMethod("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            //    var handler = Delegate.CreateDelegate(typeof(ActionDelegate), null, eventsMethod);
            //    foreach (var ev in EventAddMethods)
            //    {
            //        ev.Value?.Invoke(this, new object[] { handler });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}


        }

        protected void UninstallEvents()
        {
        }

    }
}
