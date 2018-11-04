using System;
using System.Threading.Tasks;
using sdModels = SmartDose.RestDomain.Models.V1;
using sdCrud = SmartDose.RestClient.Cruds.V1;

namespace SmartDose.RestClient.ConsoleSample.Rest.V1.MasterData
{
    public static class MasterData
    {
        public async static Task RestTest()
        {
            {
                if (await sdCrud.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
            }

            {
                if (await sdCrud.MasterData.Medicine.Instance.CreateAsync(new sdModels.MasterData.Medicine
                {
                    Identifier = "4711.V1",
                    Name = "Name 4711.V1"
                }) is var response && response.Ok)
                {
                    Console.WriteLine($"response.Data => ResultSet");
                    Console.WriteLine($"{response.Data}");
                    Console.WriteLine($"{response.Data.Code}");
                    Console.WriteLine($"{response.Data.Detail}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
            }

            // Read list with short responsetime
            {
                if (await sdCrud.MasterData.Medicine.Instance.ReadListAsync(TimeSpan.FromSeconds(1)) is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
            }
        }
    }
}
