using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using ModelsV2 = SmartDose.RestDomain.Models.V2;


namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class Manufacturer : CoreV2<ModelsV2.MasterData.Manufacturer>
    {
        public Manufacturer() : base(MasterDataName, nameof(Manufacturer))
        {
        }
        public static Manufacturer Instance => Instance<Manufacturer>();

        public async Task<SdrcFlurHttpResponse> AssignManufacturerToMedicineAsync(string manufacturerId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                => await Url.AppendPathSegment(manufacturerId)
                            .AppendPathSegment("MedicineCode")
                            .AppendPathSegment(medicineId)
                            .SdrcGetJsonAsync<int>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
