﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.RestClient.Extensions;

namespace SmartDose.RestClient.Cruds.V2
{
    public class CoreV2<T> : Core<T> where T : class
    {
        public CoreV2(params string[] pathSegments) : base(RestClientGlobals.UrlV2, pathSegments) { }
    }

    public class CoreV2Crud<T> : CoreV2<T> where T : class
    {
        public CoreV2Crud(params string[] pathSegments) : base(pathSegments) { }

        public async Task<SdrcFlurHttpResponse<List<T>>> ReadListAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone
                .SdrcGetJsonAsync<List<T>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<List<T>>> ReadListAsync(TimeSpan timeSpan)
            => await ReadListAsync(CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<T>> ReadAsync(string readId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(readId)
                .SdrcGetJsonAsync<T>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> ReadAsync(string readId, TimeSpan timeSpan)
            => await ReadAsync(readId, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<T>> CreateAsync(T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone
                .SdrcPostJsonAsync<T>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> CreateAsync(T value, TimeSpan timeSpan)
            => await CreateAsync(value, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<T>> UpdateAsync(string updateId, T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(updateId)
                .SdrcPutJsonAsync<T>(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> UpdateAsync(string updateId, T value, TimeSpan timeSpan)
            => await UpdateAsync(updateId, value, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<T>> DeleteAsync(string deleteId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await UrlClone.AppendPathSegment(deleteId)
                .SdrcDeleteAsync<T>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> DeleteAsync(string deleteId, TimeSpan timeSpan)
            => await DeleteAsync(deleteId, CancellationTokenFromTimeSpan(timeSpan)).ConfigureAwait(false);
    }
}
