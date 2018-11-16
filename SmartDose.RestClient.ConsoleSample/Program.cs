using System.IO;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmartDose.WcfClient;
using SmartDose.WcfClient.Services;

namespace SmartDose.RestClient.ConsoleSample
{
    class Program
    {
        public static object RestDomainDevGlobals { get; private set; }

        static async Task Main(string[] args)
        {
            await TestAsync();

            Console.WriteLine($"UrlV1={RestClientGlobals.UrlV1}");
            Console.WriteLine($"UrlV2={RestClientGlobals.UrlV2}");
            Console.WriteLine($"TimeOut={RestClientGlobals.UrlTimeSpan.TotalMilliseconds} ms");


            await Rest.V1.MasterData.MasterData.RestTest();
            await Rest.V2.MasterData.MasterData.RestTest();

            Console.WriteLine("Wait for Result and return to end");
            Console.ReadLine();
        }

        static void Test()
        {
        }

        static async Task TestAsync()
        {
            var wcfItem = new WcfItem
            {
                ConnectionString = "net.tcp://localhost:10000/MasterData/",
                ConnectionStringUse = "net.tcp://localhost:10000/MasterData/",
            };
            await Task.Delay(1);

            using (var service = new CommunicationService(wcfItem, wcfItem.ConnectionStringUse))
            {
                service.Start();
                Console.ReadLine();
            }
            Console.ReadLine();
        }

        public static JsonSerializerSettings JsonSerializerSettingsDev = new JsonSerializerSettings()
        {
            ContractResolver = new SerializableExpandableContractResolver(),
            Formatting = Formatting.Indented
        };

        public static string ToJsonFromObjectDev(object objectDev)
            => JsonConvert.SerializeObject(objectDev, settings: JsonSerializerSettingsDev);
    }

    public class SerializableExpandableContractResolver : DefaultContractResolver
    {
        public SerializableExpandableContractResolver()
        {
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (TypeDescriptor.GetAttributes(objectType).Contains(new TypeConverterAttribute(typeof(ExpandableObjectConverter))))
            {
                return CreateObjectContract(objectType);
            }
            return base.CreateContract(objectType);
        }
    }
}
