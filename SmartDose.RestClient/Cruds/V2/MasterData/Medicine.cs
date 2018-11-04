using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Cruds.V2.MasterData
{
    public class Medicine : CoreV2Crud<Models.MasterData.Medicine>
    {
        public Medicine() : base(MasterDataName, nameof(Medicine) + "s")
        {
        }

        public static Medicine Instance => Instance<Medicine>();

        public async Task<SdrcFlurHttpResponse<int>> GetCanisterCountAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments(medicineCode, "CanisterCount")
                .SdrcGetJsonAsync<int>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<int>> GetCanisterCountAsync(string medicineCode, TimeSpan timeSpan)
            => await GetCanisterCountAsync(medicineCode, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);
    }
}
