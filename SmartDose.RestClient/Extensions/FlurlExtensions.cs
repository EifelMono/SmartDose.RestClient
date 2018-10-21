using System;
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
    public static class SdrcFlurlExtensions
    {
        public static bool IsHttpStatusCode(this HttpStatusCode thisValue, HttpStatusCode httpStatusCode)
            => thisValue.Equals(httpStatusCode);
        public static bool IsHttpStatusCodeOK(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode(HttpStatusCode.OK);
        public static bool IsHttpStatusCodeUndefined(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.Undefined);
        public static bool IsHttpStatusCodeRequestTimeout(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.FlurlTimeOut);
        public static bool IsHttpStatusCodeFlurException(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.FlurlException);
        public static bool IsHttpStatusCodeException(this SdrcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)SdrcHttpStatusCode.Exception);
        public static bool HasException(this SdrcFlurHttpResponse thisValue)
            => thisValue.Exception != null;

        #region THECALL
        private async static Task<SdrcFlurHttpResponse<T>> SdrcHttpCallAsync<T>(Func<Task<T>> httpCallAsync)
        {
            var sdrcResponse = new SdrcFlurHttpResponse<T>();
            try
            {
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
                sdrcResponse.StatusCode = ex1.Call.Response?.StatusCode ?? (HttpStatusCode)(SdrcHttpStatusCode.FlurlException);
                if (ex1.Call.Response != null)
                    sdrcResponse.Message = await ex1.Call.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                sdrcResponse.Exception = ex1;
            }
            catch (Exception ex3)
            {
                sdrcResponse.StatusCode = (HttpStatusCode)(SdrcHttpStatusCode.Exception);
                sdrcResponse.Exception = ex3;
            }
            return sdrcResponse;
        }
        #endregion

        public async static Task<SdrcFlurHttpResponse<T>> SdrcGetJsonAsync<T>(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.GetJsonAsync<T>(cancellationToken, completionOption));
        public async static Task<SdrcFlurHttpResponse<T>> SdrcGetJsonAsync<T>(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.GetJsonAsync<T>(cancellationToken, completionOption));
        public async static Task<SdrcFlurHttpResponse> SdrcGetAsync(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                => await SdrcHttpCallAsync(() => url.GetAsync(cancellationToken, completionOption));
        public async static Task<SdrcFlurHttpResponse> SdrcGetAsync(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
                => await SdrcHttpCallAsync(() => url.GetAsync(cancellationToken, completionOption));

        public async static Task<SdrcFlurHttpResponse> SdrcPutJsonAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.PutJsonAsync(data));
        public async static Task<SdrcFlurHttpResponse> SdrcPutJsonAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.PutJsonAsync(data));

        public async static Task<SdrcFlurHttpResponse> SdrcPostJsonAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.PostJsonAsync(data, cancellationToken, completionOption));
        public async static Task<SdrcFlurHttpResponse> SdrcPostJsonAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.PostJsonAsync(data, cancellationToken, completionOption));
        public async static Task<SdrcFlurHttpResponse> SdrcPostJsonAsync(this IFlurlRequest flurlRequest, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        => await SdrcHttpCallAsync(() => flurlRequest.PostJsonAsync(data, cancellationToken, completionOption));

        public async static Task<SdrcFlurHttpResponse<HttpResponseMessage>> SdrcDeleteAsync(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.DeleteAsync(cancellationToken, completionOption));
        public async static Task<SdrcFlurHttpResponse<HttpResponseMessage>> SdrcDeleteAsync(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await SdrcHttpCallAsync(() => url.DeleteAsync(cancellationToken, completionOption));
    }
}
