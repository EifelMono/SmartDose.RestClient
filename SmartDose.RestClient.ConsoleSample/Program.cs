using System;
using System.Threading.Tasks;

namespace SmartDose.RestClient.ConsoleSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(UrlConfig.UrlV1);
            await Rest.V1.MasterData.MasterData.RestTest();

            Console.WriteLine(UrlConfig.UrlV2);
            await Rest.V2.MasterData.MasterData.RestTest();

            Console.WriteLine("Wait for Result and return to end");
            Console.ReadLine();
        }
    }
}
