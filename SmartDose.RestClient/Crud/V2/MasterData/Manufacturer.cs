using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;


namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class Manufacturer : CoreCrudV2<Models.MasterData.Manufacturer>
    {
        public Manufacturer() : base(MasterDataName, nameof(Manufacturer))
        {
        }
        public static Manufacturer Instance => Instance<Manufacturer>();

        public async Task<SdrcFlurHttpResponse> AssignManufacturerToMedicineAsync(string manufacturerId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                => await UrlClone.AppendPathSegment(manufacturerId)
                            .AppendPathSegment("AssignMedicine")
                            .AppendPathSegment(medicineId)
                            .SdrcPutJsonAsync(null, cancellationToken, completionOption).ConfigureAwait(false);
    }
}
