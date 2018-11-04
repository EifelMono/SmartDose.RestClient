using System;
using Flurl;
using Flurl.Http;
using SmartDose.RestClient.Cruds;

namespace SmartDose.RestClient
{
    public static class RestClientGlobals
    {
        public static string UrlV1 = "http://127.0.0.1:6040/SmartDose";
        public static string UrlV1Clone
        {
            get => new Url(UrlV1);
        }
        // http://localhost:6040/SmartDose/Customers
        public static string UrlV2 = "http://127.0.0.1:56040/SmartDose/V2.0";
        public static string UrlV2Clone
        {
            get => new Url(UrlV2);
        }

        public static TimeSpan UrlTimeSpan
        {
            get
            {
                TimeSpan result = TimeSpan.FromSeconds(100);
                FlurlHttp.Configure(settings =>
                {
                    if (settings.Timeout != null)
                        result = (TimeSpan)settings.Timeout;
                });
                return result;
            }
            set
            {
                FlurlHttp.Configure(settings =>
                {
                    settings.Timeout = value;
                });
            }
        }

        public static void ClearUrls()
        {
            // reset the the instance vars....
            Core.ClearInstances();
        }
    }
}

