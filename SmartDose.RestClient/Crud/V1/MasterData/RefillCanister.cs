﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;


namespace SmartDose.RestClient.Crud.V1.MasterData
{
    public class RefillCanister : CoreV1<Models.MasterData.Canister>
    {
        public RefillCanister() : base("RefillCanister")
        {
        }
        public static RefillCanister Instance => Instance<RefillCanister>();

        public async Task<SdrcFlurHttpResponse<Models.MasterData.CanisterStatus>> ReadCanisterStatusAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("Status").AppendPathSegment(identifier).SdrcGetJsonAsync<Models.MasterData.CanisterStatus>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<List<Models.MasterData.CanisterStatus>>> ReadListCanisterStatusAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("Status").SdrcGetJsonAsync<List<Models.MasterData.CanisterStatus>>(cancellationToken, completionOption).ConfigureAwait(false);

        // SdrcGetJsonAsync<Models.ResultSet> 
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> AssignCanisterAsync(string canisterId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(canisterId).AppendPathSegment("Medicine").AppendPathSegment(medicineId).SdrcPostJsonAsync<Models.ResultSet>(null, cancellationToken, completionOption).ConfigureAwait(false);


    }
}
