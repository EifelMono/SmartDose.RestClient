# SmartDose.RestClient

## SmartDose.RestDomain
* Model definitions for V1 and V2
* NetStandard 2.0

## SmartDose.RestClient

* Client to Connecto to the SmartDose.Server
* NetStandard 2.0







## SmartDose.RestClientApp

* Test App for testing the Rest interface

## SmartDose.RestClient.ConsoleSample

* Console samples V1

```csharp
using System;
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
```

* Console samples V2

```csharp
using System;
using System.Threading.Tasks;
using xModels = SmartDose.RestDomain.Models.V2;
using xCrud = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClient.ConsoleSample.Rest.V2.MasterData
{
    public static class MasterData
    {
        public async static Task RestTest()
        {
            {
                if (await xCrud.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                        Console.WriteLine($"{medicine.MedicineName} {medicine.MedicineCode}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }

            {
                if (await xCrud.MasterData.Medicine.Instance.CreateAsync(new xModels.MasterData.Medicine
                {
                    MedicineCode = "4711.V2",
                    MedicineName = "Name 4711.V2"
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

```