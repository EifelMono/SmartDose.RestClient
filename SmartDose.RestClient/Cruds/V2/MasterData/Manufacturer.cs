using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Cruds.V2.MasterData
{
    public class Manufacturer : CoreV2Crud<Models.MasterData.Manufacturer>
    {
        public Manufacturer() : base(MasterDataName, nameof(Manufacturer))
        {
        }
        public static Manufacturer Instance => Instance<Manufacturer>();
        public async Task<SdrcFlurHttpResponse> AssignManufacturerToMedicineAsync(string manufacturerId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments(manufacturerId, "AssignMedicine", medicineId)
                .SdrcPutJsonAsync(null, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse> AssignManufacturerToMedicineAsync(string manufacturerId, string medicineId, TimeSpan timeSpan)
            => await AssignManufacturerToMedicineAsync(manufacturerId, medicineId, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);
    }
}
