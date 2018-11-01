using System;
using System.Threading.Tasks;
using sdModels = SmartDose.RestDomain.Models.V2;
using sdCruds = SmartDose.RestClient.Cruds.V2;

namespace SmartDose.RestClient.ConsoleSample.Rest.V2.MasterData
{
    public static class MasterData
    {
        public async static Task RestTest()
        {
            {
                if (await sdCruds.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                        Console.WriteLine($"{medicine.MedicineName} {medicine.MedicineCode}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }

            {
                if (await sdCruds.MasterData.Medicine.Instance.CreateAsync(new sdModels.MasterData.Medicine
                {
                    MedicineCode = "4711.V2",
                    MedicineName = "Name 4711.V2"
                }) is var response && response.Ok)
                {
                    Console.WriteLine($"response.Data => Medicine");
                    Console.WriteLine($"{response.Data}");
                    Console.WriteLine($"{response.Data.MedicineCode}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }
        }
    }
}
