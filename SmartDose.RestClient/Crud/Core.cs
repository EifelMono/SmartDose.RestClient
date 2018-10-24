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
        public const string MasterDataName = "MasterData";
        public const string InventoryName = "Inventory";
        public const string ProductionName = "Production";

        public Url Url
        {
            get => new Url(UrlTemplate);
        }

        public Url UrlTemplate { get; set; }

        public Core()
        {
        }

        public Core(Url url, params string[] pathSegments) : this()
        {
            UrlTemplate = url;
            foreach (var pathSegment in pathSegments)
                UrlTemplate = UrlTemplate.AppendPathSegment(pathSegment);
        }
    }

    public class Core<T> : Core where T : class
    {
        public Core(string url, params string[] pathSegments) : base(new Url(url), pathSegments)
        {
        }

        protected static object S_Instance;
        public static Tx Instance<Tx>() where Tx : Core<T>, new()
        {
            if (S_Instance == null)
                S_Instance = new Tx();
            return (Tx)S_Instance;
        }
    }

    public class CoreCrud<T> : Core<T> where T : class
    {
        public CoreCrud(string url, params string[] pathSegments) : base(new Url(url), pathSegments)
        {
        }

        public async Task<SdrcFlurHttpResponse<List<T>>> ReadListAsync(CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
             => await Url.SdrcGetJsonAsync<List<T>>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse<T>> ReadAsync(string searchId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
          => await Url.AppendPathSegment(searchId).SdrcGetJsonAsync<T>(cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse> CreateAsync(T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.SdrcPostJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse> UpdateAsync(string updateId, T value, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment(updateId).SdrcPutJsonAsync(value, cancellationToken, completionOption).ConfigureAwait(false);
        public async Task<SdrcFlurHttpResponse> DeleteAsync(string valueId, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await Url.AppendPathSegment(valueId).SdrcDeleteAsync(cancellationToken, completionOption).ConfigureAwait(false);
    }
}
