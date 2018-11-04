using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Cruds.V1
{
    public class CoreV1<T> : Core<T> where T : class
    {
        public CoreV1(params string[] pathSegments) : base(RestClientGlobals.UrlV1, pathSegments) { }
    }

    public class CoreV1Crud<T> : CoreV1<T> where T : class
    {
        public CoreV1Crud(params string[] pathSegments) : base(pathSegments) { }

        public async Task<SdrcFlurHttpResponse<List<T>>> ReadListAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
             => await UrlClone
                .SdrcGetJsonAsync<List<T>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<List<T>>> ReadListAsync(TimeSpan timeSpan, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
              => await ReadListAsync(CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<T>> ReadAsync(string readId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await UrlClone
                .AppendPathSegment(readId).SdrcGetJsonAsync<T>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> ReadAsync(string readId, TimeSpan timeSpan, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await ReadAsync(readId, CancellationTokenFromTimeSpan(timeSpan), completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> CreateAsync(T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone
                .SdrcPostJsonAsync<Models.ResultSet>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> CreateAsync(T value, TimeSpan timeSpan, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await CreateAsync(value, CancellationTokenFromTimeSpan(timeSpan), completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> UpdateAsync(string updateId, T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(updateId)
                .SdrcPutJsonAsync<Models.ResultSet>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> UpdateAsync(string updateId, T value, TimeSpan timeSpan, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UpdateAsync(updateId, value, CancellationTokenFromTimeSpan(timeSpan), completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> DeleteAsync(string deleteId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(deleteId)
                .SdrcDeleteAsync<Models.ResultSet>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<Models.ResultSet>> DeleteAsync(string deleteId, TimeSpan timeSpan, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await DeleteAsync(deleteId, CancellationTokenFromTimeSpan(timeSpan), completionOption).ConfigureAwait(false);
    }
}
