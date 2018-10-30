using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using sdModelsV1 = SmartDose.RestDomain.Models.V1;
using sdCrudV1 = SmartDose.RestClient.Crud.V1;
using sdModelsV2 = SmartDose.RestDomain.Models.V2;
using sdCrudV2 = SmartDose.RestClient.Crud.V2;

namespace dotnet.core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(SmartDose.RestClient.UrlConfig.UrlV1);
            Console.WriteLine(SmartDose.RestClient.UrlConfig.UrlV2);

            {
                if (await sdCrudV1.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                    {
                        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
                    }
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }

            {
                if (await sdCrudV2.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                    {
                        Console.WriteLine($"{medicine.MedicineName} {medicine.MedicineCode}");
                    }
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }
        }
    }
}
