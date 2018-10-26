using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;


namespace SmartDose.RestClient.Crud.V1.MasterData
{
    public class RefillCanister : CoreV1<Models.MasterData.CanisterRefillInformation>
    {
        public RefillCanister() : base("RefillCanister")
        {
        }
        public static RefillCanister Instance => Instance<RefillCanister>();

        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> RefillCanisterAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(identifier).SdrcGetJsonAsync<Models.ResultSet>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> RefillCanisterFromStockbottleAsync(string canisterId, string containerId, string amount, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(canisterId).AppendPathSegment("MedContainer").AppendPathSegment(containerId).AppendPathSegment(amount)
                    .SdrcPostJsonAsync<Models.ResultSet>(null, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> AssignCanisterAsync(string canisterId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
         => await UrlClone.AppendPathSegment(canisterId).AppendPathSegment("Medicine").AppendPathSegment(medicineId)
                    .SdrcGetJsonAsync<Models.ResultSet>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
