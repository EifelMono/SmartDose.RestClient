using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.WcfClient.Services
{
    public class Communication
    {
        public Type FindAssemblyType { get; set; }
        public string EndPointAddress { get; set; }


        public Assembly ServiceAssembly { get; set; }
        public Type ServiceClientType { get; set; }
        public ICommunicationObject ServiceClient { get; set; }


        public Dictionary<string, MethodInfo> ServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        public Dictionary<string, MethodInfo> AsyncServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();
        public Dictionary<string, MethodInfo> EventServiceMethods { get; set; } = new Dictionary<string, MethodInfo>();


        public Communication(Type findAssemblyType, string endPointAddress)
        {
            FindAssemblyType = findAssemblyType;
            EndPointAddress = endPointAddress;

            ServiceAssembly = findAssemblyType.Assembly;
            ServiceClientType = FindServiceClient();
            ServiceClient = CreateServiceClient(endPointAddress);

            FindServiceClientMethods();
        }

        protected Type FindServiceClient()
            => ServiceAssembly.GetTypes()
                .Where(t => t.GetInterfaces().Where(i => i == typeof(ICommunicationObject)).Any())
                .Where(t => t.FullName.EndsWith("Client")).FirstOrDefault();
        protected void FindServiceClientMethods()
        {
            ServiceMethods = new Dictionary<string, MethodInfo>();
            foreach (var m in ServiceClientType.GetMethods())
            {
                ServiceMethods[m.Name] = m;
                if (m.Name.EndsWith("Async"))
                    AsyncServiceMethods[m.Name] = m;
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
                for (var i = 0; i < paramValues.Count(); i++)
                {
                    var p = paramValues[i];
                    if (p != null)
                    {
                        if (p.GetType().FullName.EndsWith("PageFilter"))
                        {
                            var x = (uint)p.GetType().GetProperty("PageSize").GetValue(p);
                            if (x == 0)
                                paramValues[i] = null;
                        }
                        else
                        if (p.GetType().FullName.EndsWith("SortFilter"))
                        {
                            var x = (string)p.GetType().GetProperty("AttributeName").GetValue(p);
                            if (string.IsNullOrEmpty(x))
                                paramValues[i] = null;
                        }
                    }
                }
                return (true, await (dynamic)m.Invoke(ServiceClient, paramValues));
            }
            catch(Exception ex)
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
                    if (p.ParameterType.IsArray)
                    {
                        objects.Add(Activator.CreateInstance(p.ParameterType, 0));
                    }
                    else
                    {
                        objects.Add(Activator.CreateInstance(p.ParameterType));
                    }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return objects.ToArray(); ;
        }
    }
}
