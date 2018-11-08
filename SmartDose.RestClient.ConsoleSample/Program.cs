using System;
using System.Globalization;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using SmartDose.WcfClient.MasterData;

namespace SmartDose.RestClient.ConsoleSample
{
    class Program
    {
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
            WcfClient.Test.FindServiceClientMethods();
            var o = WcfClient.Test.CreateServiceClient("net.tcp://LWDEU08DTK2PH2:9002/MasterData");
            var m = (MasterDataServiceClient)o;
            var c = await m.GetMedicinesAsync(new SearchFilter[] { }, null, null);
            WcfClient.Test.CreateServiceReference();
            Console.WriteLine("Wait for Result and return to end");
            Console.ReadLine();
        }
    }
}
