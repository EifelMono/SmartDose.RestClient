using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Flurl;
using Flurl.Http;
using SmartDose.RestClient.Crud;

namespace SmartDose.RestClient
{
    public static class UrlConfig
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

        public static TimeSpan? UrlTimeout
        {
            get
            {
                TimeSpan? result = null;
                FlurlHttp.Configure(settings =>
                {
                    result = settings.Timeout;
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

