﻿using System;
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

        protected static object s_Instance;
        public static Tx Instance<Tx>() where Tx : Core<T>, new()
        {
            if (s_Instance == null)
                s_Instance = new Tx();
            return (Tx)s_Instance;
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
