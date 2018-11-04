#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.Http.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Newtonsoft.Json.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestClient.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestDomain.dll"

using System;
using Newtonsoft.Json;
using sdModelsV1 = SmartDose.RestDomain.Models.V1;
using sdCrudsV1 = SmartDose.RestClient.Cruds.V1;
using sdModelsV2 = SmartDose.RestDomain.Models.V2;
using sdCrudsV2 = SmartDose.RestClient.Cruds.V2;

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

#region Setup/Teardown
Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});
#endregion

#region Tasks
Task("ShowGlobalUrlInfos")
.Does(() => {
   Information($"UrlV1={SmartDose.RestClient.RestClientGlobals.UrlV1}");
   Information($"UrlV2={SmartDose.RestClient.RestClientGlobals.UrlV2}");
   Information($"TimeOut={SmartDose.RestClient.RestClientGlobals.UrlTimeSpan.TotalMilliseconds} ms");
});

Task("GetMedicineV1")
.Does(async () => {
    if (await sdCrudsV1.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
    {
        foreach (var medicine in response.Data)
        {
            Information($"{medicine.Name} {medicine.Identifier}");
        }
    }
    else
        Information($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
});

Task("GetMedicineV1Timeout")
.Does(async () => {
    if (await sdCrudsV1.MasterData.Medicine.Instance.ReadListAsync(TimeSpan.FromSeconds(1)) is var response && response.Ok)
    {
        foreach (var medicine in response.Data)
        {
            Information($"{medicine.Name} {medicine.Identifier}");
        }
    }
    else
        Information($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
});

Task("GetMedicineV2")
.Does(async () => {
    if (await sdCrudsV2.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
    {
        foreach (var medicine in response.Data)
        {
            Information($"{medicine.MedicineName} {medicine.MedicineCode}");
        }
    }
    else
        Information($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
});

Task("Default")
.Does(() => {
   Information("Hello Cake!");
});
#endregion

RunTarget(target);