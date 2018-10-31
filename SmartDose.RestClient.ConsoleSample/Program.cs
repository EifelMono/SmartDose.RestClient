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
            Console.WriteLine($"TimeOut={RestClientGlobals.UrlTimeout?.TotalMilliseconds ?? -1} ms");

            Test();
            await TestAsync();

            await Rest.V1.MasterData.MasterData.RestTest();
            await Rest.V2.MasterData.MasterData.RestTest();

            Console.WriteLine("Wait for Result and return to end");
            Console.ReadLine();
        }

        static void Test()
        {
            try
            {
                Console.WriteLine("CULTURE ISO ISO WIN DISPLAYNAME                              ENGLISHNAME");
                foreach (var ci in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
                {
                    Console.Write("{0,-7}", ci.Name);
                    Console.Write(" {0,-3}", ci.TwoLetterISOLanguageName);
                    Console.Write(" {0,-3}", ci.ThreeLetterISOLanguageName);
                    Console.Write(" {0,-3}", ci.ThreeLetterWindowsLanguageName);
                    Console.Write(" {0,-40}", ci.DisplayName);
                    Console.WriteLine(" {0,-40}", ci.EnglishName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Test()");
            Console.ReadLine();
        }

        static async Task TestAsync()
        {
            await Task.Delay(1);
        }
    }
}
