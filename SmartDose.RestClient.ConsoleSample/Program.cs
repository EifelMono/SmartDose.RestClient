using System;
using System.Diagnostics;
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
            var com = new WcfClient.Services.Communication(typeof(WcfClient.Test), "net.tcp://LWDEU08DTK2PH2:9002/MasterData");
            var m = (MasterDataServiceClient)com.ServiceClient;
            {
                var a = await m.GetMedicinesAsync(new SearchFilter[] { }, null, null);
                m.SetMedicinesReceived += (s, e) =>
                {
                    Debug.WriteLine("x");
                };
                m.SetManufacturersReceived += (s, e) =>
                {
                    Debug.WriteLine("x");
                };
                m.DeleteMedicinesReceived += (s, e) =>
                 {
                     Debug.WriteLine("x");
                 };
                //await m.DeleteMedicineAsync(4711);
                //try
                //{
                //    await m.DeleteMedicineAsync(-1);
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(ex);
                //}

            }

            {
                com.OnEvents = (sender, eventArgs) =>
                 {
                     Debug.WriteLine("x");
                 };
                await Task.Delay(10000000);


                var ps = com.GetMethodsParamsObjects("GetMedicinesAsync");

                if (await com.CallMethodAsync("GetMedicinesAsync", ps) is var result0 && result0.Ok)
                    Debug.WriteLine(result0.Value);

                if (await com.CallMethodAsParamsAync("GetMedicinesAsync", new SearchFilter[] { }, null, null) is var result1 && result1.Ok)
                    Debug.WriteLine(result1.Value);

                if (await com.CallMethodAsParamsAync("DeleteMedicineAsync", 4711) is var result2 && result2.Ok)
                {
                    Debug.WriteLine("Ok");
                }
                if (await com.CallMethodAsParamsAync("DeleteMedicineAsync", -1) is var result3 && !result3.Ok)
                {
                    Debug.WriteLine("error");
                }
            }

            /*
            WcfClient.Test.FindServiceClientMethods();
            var o = WcfClient.Test.CreateServiceClient("net.tcp://LWDEU08DTK2PH2:9002/MasterData");
            var m = (MasterDataServiceClient)o;
            var c = await m.GetMedicinesAsync(new SearchFilter[] { }, null, null);
            WcfClient.Test.CreateServiceReference();
            */
            Console.WriteLine("Wait for Result and return to end");
            Console.ReadLine();
        }
    }
}
