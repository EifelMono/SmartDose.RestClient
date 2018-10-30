#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.Http.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Newtonsoft.Json.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestClient.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestDomain.dll"

using Newtonsoft.Json;
using sdModelsV1 = SmartDose.RestDomain.Models.V1;
using sdCrudV1 = SmartDose.RestClient.Crud.V1;
using sdModelsV2 = SmartDose.RestDomain.Models.V2;
using sdCrudV2 = SmartDose.RestClient.Crud.V2;

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

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

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("ShowUrls")
.Does(() => {
   Information($"UrlV1={SmartDose.RestClient.UrlConfig.UrlV1}");
   Information($"UrlV2={SmartDose.RestClient.UrlConfig.UrlV2}");
});

Task("GetMedicineV1")
.Does(async () => {
    if (await sdCrudV1.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
    {
        foreach (var medicine in response.Data)
        {
            Information($"{medicine.Name} {medicine.Identifier}");
        }
    }
    else
        Information($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
});

Task("GetMedicineV2")
.Does(async () => {
    if (await sdCrudV2.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
    {
        foreach (var medicine in response.Data)
        {
            Information($"{medicine.MedicineName} {medicine.MedicineCode}");
        }
    }
    else
        Information($"{response.Request}\r\n{response.StatusCode}\r\n{response.Message}\r\n{response.Data}");
});


Task("Default")
.Does(() => {
   Information("Hello Cake!");
});

RunTarget(target);