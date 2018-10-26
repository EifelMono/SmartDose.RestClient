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
    public class Canister : CoreV1<Models.MasterData.Canister>
    {
        public Canister() : base(nameof(Canister) + "s")
        {
        }

        public static Canister Instance => Instance<Canister>();

        public async Task<SdrcFlurHttpResponse<List<Models.MasterData.Canister>>> ReadListAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.SdrcGetJsonAsync<List<Models.MasterData.Canister>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.MasterData.Canister>> ReadAsync(string readId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(readId).SdrcGetJsonAsync<Models.MasterData.Canister>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> CreateAsync(Models.MasterData.Canister value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.SdrcPostJsonAsync<Models.ResultSet>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> UpdateAsync(string updateId, Models.MasterData.Canister value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(updateId).SdrcPutJsonAsync<Models.ResultSet>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> DeleteAsync(string deleteId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(deleteId).SdrcDeleteAsync<Models.ResultSet>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.MasterData.CanisterStatus>> ReadCanisterStatusAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                => await UrlClone.AppendPathSegment("Status").AppendPathSegment(identifier).SdrcGetJsonAsync<Models.MasterData.CanisterStatus>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<List<Models.MasterData.CanisterStatus>>> ReadListCanisterStatusAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("Status").SdrcGetJsonAsync<List<Models.MasterData.CanisterStatus>>(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> AssignCanisterAsync(string canisterId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(canisterId).AppendPathSegment("Medicine").AppendPathSegment(medicineId).SdrcPostJsonAsync<Models.ResultSet>(null, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> ActivateCanisterAsync(string canisterId, bool state, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
             => await UrlClone.AppendPathSegment(canisterId).AppendPathSegment("Activate").AppendPathSegment(state).SdrcPostJsonAsync<Models.ResultSet>(null, cancellationToken, completionOption).ConfigureAwait(false);
    }
}
