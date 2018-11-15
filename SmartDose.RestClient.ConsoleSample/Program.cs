using System;
using System.ComponentModel;
using System.IO;
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

            SmartDoseWcfClientGlobals.ExtractCakeBuilds();


            SmartDoseWcfClientGlobals.ExtractCakeBuilds();
            var clients = SmartDoseWcfClientGlobals.FindWcfClients();
            var copied= SmartDoseWcfClientGlobals.CopyWcfClientsToDirectory();
            var connectionString = "net.tcp://lwdeu08dtk2ph2:10000/MasterData/";
            var connectionAssembly = @"C:\ProgramData\Rowa\Bin\SmartDose.RestClientApp\WcfClients\lwdeu08dtk2ph2_10000_MasterData\bin\Debug\netstandard2.0\lwdeu08dtk2ph2_10000_MasterData.dll";
            try
            {
                string fileToLoad = @"C:\Dev\CSharp\Projects\smartdose\Source\Projects\SmartDose.RestClient\SmartDose.RestClient.ConsoleSample\bin\Debug\net471\lwdeu08dtk2ph2_10000_MasterData.dll";
                // string fileToLoad = @"C:\Dev\CSharp\Projects\smartdose\Source\Projects\SmartDose.RestClient\SmartDose.RestClient.ConsoleSample\bin\Debug\x\lwdeu08dtk2ph2_10000_MasterData.dll";
                var domaininfo = new AppDomainSetup();
                // domaininfo.ApplicationBase = Path.GetDirectoryName(fileToLoad);
                domaininfo.ApplicationBase = Directory.GetCurrentDirectory();
                Evidence adEvidence = AppDomain.CurrentDomain.Evidence;
                AppDomain domain = AppDomain.CreateDomain("Domain2", adEvidence, domaininfo);
                AssemblyName assamblyName = AssemblyName.GetAssemblyName(fileToLoad);
                var assembly= domain.Load(assamblyName);
                var s = assembly.GetTypes();
                AppDomain.Unload(domain);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            using (var client = new CommunicationService(connectionAssembly, connectionString))
            {
                client.Start();
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
