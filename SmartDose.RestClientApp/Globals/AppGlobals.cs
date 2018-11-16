using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using SmartDose.RestClient;
using SmartDose.Core;
using SmartDose.WcfClient;
using SmartDose.Core.Extensions;

namespace SmartDose.RestClientApp.Globals
{
    public static class AppGlobals
    {
        public static void Init()
        {
            Configuration.Load();
            if (!Configuration.FileExists)
                Configuration.Save();
            var done = WcfClientGlobals.CopyWcfClientAssembliesToAppExecutionDirectory();
            "WcfClients copied".LogInformation();
            done.Copied.ForEach(d => d.LogInformation());
            "WcfClients not copied".LogInformation();
            done.NotCopied.ForEach(d => d.LogInformation());
        }

        public static class Configuration
        {
            public static string FileName => Path.Combine(CoreGlobals.DataBinDirectory, $"{CoreGlobals.AppName}.json");

            public static ConfigurationData Data = new ConfigurationData();

            public static bool FileExists
                => File.Exists(FileName);

            public static void Load()
            {
                try
                {
                    if (FileExists)
                        Data = File.ReadAllText(FileName).ToExpandableObjectFromJson<ConfigurationData>();
                    else
                    {
                        Data.UrlV1 = "http://localhost:6040/SmartDose";
                        // http://localhost:6040/SmartDose/Customers


                        Data.UrlV2 = "http://localhost:56040/SmartDose/V2.0";
                        // http://localhost:56040/SmartDose/V2.0/MasterData/Customers
                        // http://localhost:56040/SmartDose/V2.0/swagger/docs/v2

                        Data.UrlTimeSpan = TimeSpan.FromSeconds(100);

                        Data.WcfTimeSpan = TimeSpan.FromSeconds(100);

                        Data.WcfClients = new System.Collections.Generic.List<WcfItem>
                        {
                            new WcfItem
                            {
                                ConnectionString="net.tcp://localhost:10000/MasterData/",
                                ConnectionStringUse= "net.tcp://localhost:10000/MasterData/",
                                Active=true
                            },
                            new WcfItem
                            {
                                ConnectionString="net.tcp://localhost:10000/Settings/",
                                ConnectionStringUse= "net.tcp://localhost:10000/Settings/",
                                Active=true
                            }
                        };
                    }

                    RestClientGlobals.UrlV1 = Data.UrlV1;
                    RestClientGlobals.UrlV2 = Data.UrlV2;
                    RestClientGlobals.UrlTimeSpan = Data.UrlTimeSpan;
                }
                catch (Exception ex)
                {
                    Data = new ConfigurationData();
                    Debug.WriteLine(ex);
                }
            }

            public static void Save()
            {
                try
                {
                    File.WriteAllText(FileName, Data.ToJsonFromExpandableObject());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

        }
    }
}
