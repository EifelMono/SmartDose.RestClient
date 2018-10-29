#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Flurl.Http.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\Newtonsoft.Json.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestClient.dll"
#r "..\..\SmartDose.RestClientApp\bin\Debug\SmartDose.RestDomain.dll"

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
