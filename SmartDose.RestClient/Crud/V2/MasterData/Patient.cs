﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using ModelsV2 = SmartDose.RestDomain.Models.V2;


namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class Patient : CoreV2<ModelsV2.MasterData.Patient>
    {
        public Patient() : base(MasterDataName, nameof(Patient) + "s")
        {
        }

        public static Patient Instance => Instance<Patient>();
        public async Task<SdrcFlurHttpResponse<int>> GetCanisterCountAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                => await Url.AppendPathSegment(medicineCode).AppendPathSegment("CanisterCount").SdrcGetJsonAsync<int>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}