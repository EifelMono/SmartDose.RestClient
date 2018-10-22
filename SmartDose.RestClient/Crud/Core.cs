using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using SmartDose.RestClient.Extensions;

namespace SmartDose.RestClient.Crud
{
    public class Core
    {
        public Url Url { get; set; }

        public Core()
        {
        }

        public Core(Url url, params string[] pathSegments) : this()
        {
            Url = url;
            foreach (var pathSegment in pathSegments)
                Url = Url.AppendPathSegment(pathSegment);
        }
    }

    public class Core<T> : Core where T : class
    {
        public Core(string url, params string[] pathSegments) : base(new Url(url), pathSegments)
        {
        }



        public async Task<SdrcFlurHttpResponse> CreateAsync(T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.SdrcPostJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse> DeletetAsync(string medicineCode, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment(medicineCode).SdrcDeleteAsync(cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse> UpdateAsync(string updateId, T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
         => await Url.AppendPathSegment(updateId).SdrcPostJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);

        public async Task<SdrcFlurHttpResponse<List<T>>> GetListAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await Url.SdrcGetJsonAsync<List<T>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> GetAsync(string searchId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await Url.AppendPathSegment(searchId).SdrcGetJsonAsync<T>(cancellationToken, completionOption).ConfigureAwait(false);
    }

    public class CoreV1<T> : Core<T> where T : class
    {
        public CoreV1(params string[] pathSegments) : base(UrlConfig.UrlV1, pathSegments)
        {

        }
    }

    public class CoreV2<T> : Core<T> where T : class
    {
        public CoreV2(params string[] pathSegments) : base(UrlConfig.UrlV2, pathSegments)
        {

        }
    }
}
