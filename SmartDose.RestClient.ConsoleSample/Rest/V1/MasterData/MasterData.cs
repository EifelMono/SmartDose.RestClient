﻿using System;
using System.Threading.Tasks;
using xModels = SmartDose.RestDomain.Models.V1;
using xCrud = SmartDose.RestClient.Crud.V1;

namespace SmartDose.RestClient.ConsoleSample.Rest.V1.MasterData
{
    public static class MasterData
    {
        public async static Task RestTest()
        {
            {
                if (await xCrud.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }

            {
                if (await xCrud.MasterData.Medicine.Instance.CreateAsync(new xModels.MasterData.Medicine
                {
                    Identifier = "4711.V1",
                    Name = "Name 4711.V1"
                }) is var response && response.Ok)
                {
                    Console.WriteLine($"{response.Data}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }
        }
    }
}