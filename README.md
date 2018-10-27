# SmartDose.RestClient

![SmartDose.RestClientApp](./docs/SmartDose.RestClientApp.png?raw=true)

## SmartDose.RestDomain
* Model definitions for V1 and V2
* NetStandard 2.0

## SmartDose.RestClient

* Rest crud clients to connect to the SmartDose.Server
* NetStandard 2.0

## SmartDose.RestClientApp

* Net 4.71 needed
* Test App for testing the Rest interface

## SmartDose.RestClient.ConsoleSample

* Console samples V1

```csharp
using System;
using System.Threading.Tasks;
using sdModels = SmartDose.RestDomain.Models.V1;
using sdCrud = SmartDose.RestClient.Crud.V1;

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
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
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
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }
        }
    }
}
```

* DotNet Core Console samples V2

```csharp
using System;
using System.Threading.Tasks;
using sdModels = SmartDose.RestDomain.Models.V2;
using sdCrud = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClient.ConsoleSample.Rest.V2.MasterData
{
    public static class MasterData
    {
        public async static Task RestTest()
        {
            {
                if (await sdCrud.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                        Console.WriteLine($"{medicine.MedicineName} {medicine.MedicineCode}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }

            {
                if (await sdCrud.MasterData.Medicine.Instance.CreateAsync(new sdModels.MasterData.Medicine
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
```

# Scripting mit Roslynpad or WorksBooks

* [Rosylnpad](https://roslynpad.net/)
* [WorksBooks](https://docs.microsoft.com/de-de/xamarin/tools/workbooks/) 

```csharp
// please use the correct path
// xxxxxxxxxxxxxxxxxxxxxxxx
#r "C:\Dev\tools\RoslynLibs\Flurl.dll"
#r "C:\Dev\tools\RoslynLibs\Flurl.Http.dll"
#r "C:\Dev\tools\RoslynLibs\Newtonsoft.Json.dll"
#r "C:\Dev\tools\RoslynLibs\SmartDose.RestClient.dll"
#r "C:\Dev\tools\RoslynLibs\SmartDose.RestDomain.dll"

using Newtonsoft.Json;
using sdModels = SmartDose.RestDomain.Models.V1;
using sdCrud = SmartDose.RestClient.Crud.V1;

Console.WriteLine(SmartDose.RestClient.UrlConfig.UrlV1);
Console.WriteLine(SmartDose.RestClient.UrlConfig.UrlV2);

if (await sdCrud.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
{
    foreach (var medicine in response.Data)
    {
        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
    }
}
else
    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
```