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
            var done= SmartDoseWcfClientGlobals.CopyWcfClientsToAppDirectory();
            "WcfClients copied".LogInformation();
            done.Copied.ForEach(d => d.LogInformation());
            "WcfClients not copied".LogInformation();
            done.NotCopied.ForEach(d => d.LogInformation());
        }

        public static class Configuration
        {
            public static string FileName => Path.Combine(SmartDoseCoreGlobals.DataBinDirectory, $"{SmartDoseCoreGlobals.AppName}.json");

            public static ConfigurationData Data = new ConfigurationData();

            public static bool FileExists
                => File.Exists(FileName);

            public static void Load()
            {
                try
                {
                    if (FileExists)
                        Data = JsonConvert.DeserializeObject<ConfigurationData>(File.ReadAllText(FileName));
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
                    File.WriteAllText(FileName, JsonConvert.SerializeObject(Data, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

        }
    }
}
