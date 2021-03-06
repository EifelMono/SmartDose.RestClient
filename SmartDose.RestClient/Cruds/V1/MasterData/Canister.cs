﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Cruds.V1.MasterData
{
    public class Canister : CoreV1Crud<Models.MasterData.Canister>
    {
        public Canister() : base(nameof(Canister) + "s")
        {
        }

        public static Canister Instance => Instance<Canister>();

        public async Task<SdrcFlurHttpResponse<Models.MasterData.CanisterStatus>> GetCanisterStatusAsync(string identifier, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments("Status", identifier)
                .SdrcGetJsonAsync<Models.MasterData.CanisterStatus>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.MasterData.CanisterStatus>> GetCanisterStatusAsync(string identifier, TimeSpan timeSpan)
            => await GetCanisterStatusAsync(identifier, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<Models.MasterData.CanisterStatus>>> GetListCanisterStatusAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment("Status")
                .SdrcGetJsonAsync<List<Models.MasterData.CanisterStatus>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<List<Models.MasterData.CanisterStatus>>> GetListCanisterStatusAsync(TimeSpan timeSpan)
            => await GetListCanisterStatusAsync(CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> AssignCanisterAsync(string canisterId, string medicineId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments(canisterId, "Medicine", medicineId)
                .SdrcPostJsonAsync<Models.ResultSet>(null, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> AssignCanisterAsync(string canisterId, string medicineId, TimeSpan timeSpan)
            => await AssignCanisterAsync(canisterId, medicineId, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> ActivateCanisterAsync(string canisterId, bool state, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegments(canisterId, "Activate", state)
                .SdrcPostJsonAsync<Models.ResultSet>(null, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> ActivateCanisterAsync(string canisterId, bool state, TimeSpan timeSpan)
            => await ActivateCanisterAsync(canisterId, state, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);
    }
}
