using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SmartDose.WcfClient
{
    public static class Test
    {
        public static void CreateServiceReference()
        {
            // var p= Process.Start("dotnet", "svcutil  net.tcp://127.0.0.1:9002/MasterData -n *,SmartDose.WcfClient.MasterData");

            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = @"C:\Dev\müll\HelloSvcutil",
                    CreateNoWindow = false,
                    FileName = "cmd.exe",
                    Arguments = "/C dotnet svcutil  net.tcp://127.0.0.1:9002/MasterData -n *,HellWorld",
                }
            };
            p.Start();
        }

        public static Type FindServiceClient()
            => typeof(Test).Assembly.GetTypes()
                .Where(a => a.GetInterfaces().Where(i => i == typeof(ICommunicationObject)).Any())
                .Where(t => t.FullName.EndsWith("Client")).FirstOrDefault();

        public static object CreateServiceClient(string endPointAddress)
            => Activator.CreateInstance(FindServiceClient(),
                    new NetTcpBinding(SecurityMode.None),
                    new EndpointAddress(endPointAddress));

        public static void FindServiceClientMethods()
        {
            var sb = new StringBuilder();
            foreach (var m in FindServiceClient().GetMethods())
            {
                sb.AppendLine($"{m.IsPublic} {m.IsConstructor} {m.Name}");
                foreach (var p in m.GetParameters())
                {
                    sb.AppendLine($"  {p.Name} {p.ParameterType.Name}");
                }

            }

            var s = sb.ToString();
            Debug.WriteLine(sb.ToString());
        }
    }
}
