#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.Http.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Newtonsoft.Json.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestClient.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestDomain.dll"

using Newtonsoft.Json;
using sdModelsV1 = SmartDose.RestDomain.Models.V1;
using sdCrudsV1 = SmartDose.RestClient.Cruds.V1;
using sdModelsV2 = SmartDose.RestDomain.Models.V2;
using sdCrudsV2 = SmartDose.RestClient.Cruds.V2;

Console.WriteLine($"UrlV1={SmartDose.RestClient.RestClientGlobals.UrlV1}");
Console.WriteLine($"UrlV2={SmartDose.RestClient.RestClientGlobals.UrlV2}");
Console.WriteLine($"TimeOut={SmartDose.RestClient.RestClientGlobals.UrlTimeSpan.TotalMilliseconds} ms");
{
	if (await sdCrudV1.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
    {
		foreach (var medicine in response.Data)
        {
			Console.WriteLine($"{medicine.Name} {medicine.Identifier}");
        }
    }
    else
		Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
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
		Console.WriteLine($"{response.Request}\r\n{response.StatusCodeAsString}\r\n{response.Message}\r\n{response.Data}");
}
