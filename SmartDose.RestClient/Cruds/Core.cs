using System;
using System.Collections.Generic;
using System.Threading;
using Flurl;

namespace SmartDose.RestClient.Cruds
{
    public class Core
    {
        public const string MasterDataName = "MasterData";
        public const string InventoryName = "Inventory";
        public const string ProductionName = "Production";

        public Url UrlClone
        {
            get => new Url(Url);
        }

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

        protected CancellationToken CancellationTokenFromTimeSpan(TimeSpan timespan)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(timespan);
            return cts.Token;
        }

        public bool UseNewInstance { get; set; } = false;
        public static List<Core> S_Instances { get; set; } = new List<Core>();
        public static void ClearInstances()
        {
            lock (S_Instances)
            {
                S_Instances.ForEach(i => i.UseNewInstance = true);
                S_Instances.Clear();
            }
        }
    }

    public class Core<T> : Core where T : class
    {
        public Core(string url, params string[] pathSegments) : base(new Url(url), pathSegments)
        {
        }

        internal static Core S_Instance;
        public static Tx Instance<Tx>() where Tx : Core<T>, new()
            => (Tx)(S_Instance is null || S_Instance.UseNewInstance ? S_Instance = AddInstance<Tx>() : S_Instance);

        private static Tx AddInstance<Tx>() where Tx : Core<T>, new()
        {
            lock (S_Instances)
            {
                var instance = new Tx();
                S_Instances.Add(instance);
                return instance;
            }
        }
    }
}
