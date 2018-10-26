using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;

namespace SmartDose.RestClient.Crud.V2
{
    public class CoreV2<T> : Core<T> where T : class
    {
        public CoreV2(params string[] pathSegments) : base(UrlConfig.UrlV2, pathSegments)
        {

        }
    }

    public class CoreCrudV2<T> : CoreV2<T> where T : class
    {
        public CoreCrudV2(params string[] pathSegments) : base(pathSegments)
        {

        }

        public async Task<SdrcFlurHttpResponse<List<T>>> ReadListAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
         => await UrlClone.SdrcGetJsonAsync<List<T>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> ReadAsync(string readId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await UrlClone.AppendPathSegment(readId).SdrcGetJsonAsync<T>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> CreateAsync(T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.SdrcPostJsonAsync<T>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> UpdateAsync(string updateId, T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(updateId).SdrcPutJsonAsync<T>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> DeleteAsync(string deleteId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(deleteId).SdrcDeleteAsync<T>(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
