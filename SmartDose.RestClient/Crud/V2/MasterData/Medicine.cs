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
    public class Medicine : CoreV2<ModelsV2.MasterData.Medicine>
    {
        public Medicine() : base(MasterDataName, nameof(Medicine) + "s")
        {
        }

        public static Medicine Instance => Instance<Medicine>();

        public async Task<SdrcFlurHttpResponse<int>> GetCanisterCountAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                => await Url.AppendPathSegment(medicineCode).AppendPathSegment("CanisterCount").SdrcGetJsonAsync<int>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
