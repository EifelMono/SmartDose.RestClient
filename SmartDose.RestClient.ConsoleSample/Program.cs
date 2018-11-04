using System;
using System.Globalization;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace SmartDose.RestClient.ConsoleSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"UrlV1={RestClientGlobals.UrlV1}");
            Console.WriteLine($"UrlV2={RestClientGlobals.UrlV2}");
            Console.WriteLine($"TimeOut={RestClientGlobals.UrlTimeSpan.TotalMilliseconds} ms");

            await TestAsync();

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
            await Task.Delay(1);
        }
    }
}
