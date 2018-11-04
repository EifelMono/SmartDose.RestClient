using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Cruds.V1.MasterData
{
    public class Manufacturer : CoreV1Crud<Models.MasterData.Manufacturer>
    {
        public Manufacturer() : base(nameof(Manufacturer))
        {
        }

        public static Manufacturer Instance => Instance<Manufacturer>();

        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> AssignManufacturerToMedicinAsync(string manufacturerId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments(manufacturerId, "Medicine", medicineId)
                .SdrcGetJsonAsync<Models.ResultSet>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> AssignManufacturerToMedicinAsync(string manufacturerId, string medicineId, TimeSpan timeSpan)
            => await AssignManufacturerToMedicinAsync(manufacturerId, medicineId, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);
    }
}
