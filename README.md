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
using sdCruds = SmartDose.RestClient.Cruds.V1;

namespace SmartDose.RestClient.ConsoleSample.Rest.V1.MasterData
{
    public static class MasterData
    {
        public async static Task RestTest()
        {
            {
                if (await sdCruds.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
                {
                    foreach (var medicine in response.Data)
                        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
                }
                else
                    Console.WriteLine($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
            }

            {
                if (await sdCruds.MasterData.Medicine.Instance.CreateAsync(new sdModels.MasterData.Medicine
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
```

# Scripting mit other tools see the folder samples

## Tools link
* [CakeBuild](https://cakebuild.net/)
        * [VsCode integration](https://cakebuild.net/docs/editors/vscode)

* [WorksBooks](https://docs.microsoft.com/de-de/xamarin/tools/workbooks/) 
* [Rosylnpad](https://roslynpad.net/)

* [Dotnet.Core](https://dot.net)

## Libraries...
```csharp
// please use the correct path
// xxxxxxxxxxxxxxxxxxxxxxxx
#r "PathTo...SmartDose.RestClientApp\bin\Debug\Flurl.dll"
#r "PathTo...SmartDose.RestClientApp\bin\Debug\Flurl.Http.dll"
#r "PathTo...SmartDose.RestClientApp\bin\Debug\Newtonsoft.Json.dll"
#r "PathTo...SmartDose.RestClientApp\bin\Debug\SmartDose.RestClient.dll"
#r "PathTo...SmartDose.RestClientApp\bin\Debug\SmartDose.RestDomain.dll"

using Newtonsoft.Json;
using sdModelsV1 = SmartDose.RestDomain.Models.V1;
using sdCrudsV1 = SmartDose.RestClient.Cruds.V1;
using sdModelsV2 = SmartDose.RestDomain.Models.V2;
using sdCrudsV2 = SmartDose.RestClient.Cruds.V2;
```

## Infos about the Lib
```csharp
    Console.WriteLine($"UrlV1={RestClientGlobals.UrlV1}");
    Console.WriteLine($"UrlV2={RestClientGlobals.UrlV2}");
    Console.WriteLine($"TimeOut={RestClientGlobals.UrlTimeSpan.TotalMilliseconds} ms");
```

## V1 get Medicine list 
```csharp
if (await sdCrudsV1.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
{
    foreach (var medicine in response.Data)
    {
        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
    }
}
else
    Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
```

## V1 get Medicine list with user wait
```csharp
if (await sdCrudsV1.MasterData.Medicine.Instance.ReadListAsync(TimeSpan.FromSeconds(10)) is var response && response.Ok)
{
    foreach (var medicine in response.Data)
    {
        Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
    }
}
else
    Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
```

## V2 get Medicine list
```csharp
if (await sdCrudsV2.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
{
    foreach (var medicine in response.Data)
    {
        Console.WriteLine($"{medicine.MedicineName} {medicine.MedicineCode}");
    }
}
else
    Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
```