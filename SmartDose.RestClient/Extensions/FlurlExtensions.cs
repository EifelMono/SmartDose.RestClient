﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace SmartDose.RestClient.Extensions
{
    // Sdrc SmartDoseRestClient
    public static class SdrcFlurlExtensions
    {
        public static bool IsHttpStatusCode(this HttpStatusCode thisValue, HttpStatusCode httpStatusCode)
            => thisValue.Equals(httpStatusCode);
        public static bool IsHttpStatusCodeOK(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode(HttpStatusCode.OK);
        public static bool IsHttpStatusCodeUndefined(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.Undefined);
        public static bool IsHttpStatusCodeFlurlTimeout(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.FlurlTimeOut);
        public static bool IsHttpStatusCodeFlurlTaskCanceled(this SdrcFlurHttpResponse thisValue)
       => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.FlurlTaskCanceled);
        public static bool IsHttpStatusCodeFlurException(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.FlurlException);
        public static bool IsHttpStatusCodeException(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.Exception);
        public static bool HasException(this SdrcFlurHttpResponse thisValue)
            => thisValue.Exception != null;

        #region THECALL
        private async static Task<SdrcFlurHttpResponse<T>> SdrcHttpCallAsync<T>(string request, Func<Task<T>> httpCallAsync)
        {
            var sdrcResponse = new SdrcFlurHttpResponse<T>();
            try
            {
                sdrcResponse.Request = request;
                sdrcResponse.Data = await httpCallAsync().ConfigureAwait(false);
                sdrcResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                sdrcResponse.StatusCode = (HttpStatusCode)SdrcHttpStatusCode.FlurlTimeOut;
                sdrcResponse.Exception = ex;
            }
            catch (FlurlHttpException ex1)
            {
                if (ex1.InnerException is TaskCanceledException)
                    sdrcResponse.StatusCode = ex1.Call.Response?.StatusCode ?? (HttpStatusCode)(SdrcHttpStatusCode.FlurlTaskCanceled);
                else
                    sdrcResponse.StatusCode = ex1.Call.Response?.StatusCode ?? (HttpStatusCode)(SdrcHttpStatusCode.FlurlException);
                if (ex1.Call?.Response?.Content != null)
                    try
                    {
                        sdrcResponse.Message = await ex1.Call.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    catch // (Exception ex11)
                    {
                        // sdrcResponse.Message = ex11.Message;
                    }
                sdrcResponse.Exception = ex1;
            }
            catch (Exception ex3)
            {
                sdrcResponse.StatusCode = (HttpStatusCode)(SdrcHttpStatusCode.Exception);
                sdrcResponse.Exception = ex3;
            }
            return sdrcResponse.MarkReceived();
        }
        private async static Task<SdrcFlurHttpResponse<T>> SdrcHttpCallAsync<T>(Url request, Func<Task<T>> httpCallAsync)
            => await SdrcHttpCallAsync(request?.ToString() ?? "", httpCallAsync).ConfigureAwait(false);
        private async static Task<SdrcFlurHttpResponse<T>> SdrcHttpCallAsync<T>(IFlurlRequest request, Func<Task<T>> httpCallAsync)
            => await SdrcHttpCallAsync(request?.ToString() ?? "", httpCallAsync).ConfigureAwait(false);

        #endregion

        #region Get
        public async static Task<SdrcFlurHttpResponse<T>> SdrcGetJsonAsync<T>(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.GetJsonAsync<T>(cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcGetJsonAsync<T>(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.GetJsonAsync<T>(cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse> SdrcGetAsync(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.GetAsync(cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse> SdrcGetAsync(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.GetAsync(cancellationToken, completionOption)).ConfigureAwait(false);
        #endregion

        #region Put
        public async static Task<SdrcFlurHttpResponse> SdrcPutJsonAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PutJsonAsync(data)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcPutJsonAsync<T>(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PutJsonAsync(data).ReceiveJson<T>()).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse> SdrcPutJsonAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PutJsonAsync(data)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcPutJsonAsync<T>(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PutJsonAsync(data).ReceiveJson<T>()).ConfigureAwait(false);
        #endregion

        #region Post
        public async static Task<SdrcFlurHttpResponse> SdrcPostJsonAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PostJsonAsync(data, cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcPostJsonAsync<T>(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PostJsonAsync(data, cancellationToken, completionOption).ReceiveJson<T>()).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse> SdrcPostJsonAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PostJsonAsync(data, cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcPostJsonAsync<T>(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.PostJsonAsync(data, cancellationToken, completionOption).ReceiveJson<T>()).ConfigureAwait(false);

        public async static Task<SdrcFlurHttpResponse> SdrcPostJsonAsync(this IFlurlRequest flurlRequest, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(flurlRequest, () => flurlRequest.PostJsonAsync(data, cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcPostJsonAsync<T>(this IFlurlRequest flurlRequest, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(flurlRequest, () => flurlRequest.PostJsonAsync(data, cancellationToken, completionOption).ReceiveJson<T>()).ConfigureAwait(false);
        #endregion

        #region Delete
        public async static Task<SdrcFlurHttpResponse> SdrcDeleteAsync(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.DeleteAsync(cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcDeleteAsync<T>(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.DeleteAsync(cancellationToken, completionOption).ReceiveJson<T>()).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse> SdrcDeleteAsync(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.DeleteAsync(cancellationToken, completionOption)).ConfigureAwait(false);
        public async static Task<SdrcFlurHttpResponse<T>> SdrcDeleteAsync<T>(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(url, () => url.DeleteAsync(cancellationToken, completionOption).ReceiveJson<T>()).ConfigureAwait(false);
        #endregion
    }
}
