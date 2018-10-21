using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;
using Domain = SmartDose.RestDomain.Models.V2.MasterData;


namespace SmartDose.RestClient.V2.MasterData
{
    public class Medicine
    {
        public static Url Url => new Url(UrlConfig.UrlV2)
                                        .AppendPathSegment(nameof(MasterData))
                                        .AppendPathSegment(nameof(Medicine) + "s");

        public static async Task<SdrcFlurHttpResponse> CreateAsync(Domain.Medicine medicine, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.SdrcPostJsonAsync(medicine, cancellationToken, completionOption).ConfigureAwait(false);

        public static async Task<SdrcFlurHttpResponse> DeletetAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment(medicineCode).SdrcDeleteAsync(cancellationToken, completionOption).ConfigureAwait(false);

        public static async Task<SdrcFlurHttpResponse> UpdateAsync(string medicineCode, Domain.Medicine medicine, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
         => await Url.AppendPathSegment(medicineCode).SdrcPostJsonAsync(medicine, cancellationToken, completionOption).ConfigureAwait(false);

        public static async Task<SdrcFlurHttpResponse<List<Domain.Medicine>>> GetAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await Url.SdrcGetJsonAsync<List<Domain.Medicine>>(cancellationToken, completionOption).ConfigureAwait(false);
        public static async Task<SdrcFlurHttpResponse<Domain.Medicine>> GetAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await Url.AppendPathSegment(medicineCode).SdrcGetJsonAsync<Domain.Medicine>(cancellationToken, completionOption).ConfigureAwait(false);

        public static async Task<SdrcFlurHttpResponse<int>> GetCanisterCountAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment(medicineCode).AppendPathSegment("CanisterCount").SdrcGetJsonAsync<int>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
