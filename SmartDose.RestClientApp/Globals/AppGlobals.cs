using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace SmartDose.RestClientApp.Globals
{
    public static class AppGlobals
    {
        public static void Init()
        {
            Configuration.Load();
            if (!Configuration.FileExists)
                Configuration.Save();
        }

        public static string AppName => Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);

        private static string _DataRowaDirectory = null;
        public static string DataRowaDirectory => _DataRowaDirectory
                                              ?? (_DataRowaDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Rowa")
                                                    .EnsureDirectoryExist());
        private static string _DataBinDirectory = null;
        public static string DataBinDirectory => _DataBinDirectory
                                              ?? (_DataBinDirectory = Path.Combine(DataRowaDirectory, "Bin", AppName)
                                                    .EnsureDirectoryExist());
        private static string _DataProtocolDirectory = null;
        public static string DataProtocolDirectory => _DataProtocolDirectory
                                              ?? (_DataProtocolDirectory = Path.Combine(DataRowaDirectory, "Protocol", AppName)
                                                    .EnsureDirectoryExist());

        public static class Log
        {
            public static string LogFileName => Path.Combine(DataProtocolDirectory, $"{DateTime.Now:yyyyMMdd}.{AppName}.Log");
        }


        public static class Configuration
        {

            public static string FileName => Path.Combine(DataBinDirectory, $"{AppName}.json");

            public static ConfigurationData Data = new ConfigurationData();

            public static bool FileExists
                => File.Exists(FileName);

            public static void Load()
            {
                try
                {
                    if (FileExists)
                        Data = JsonConvert.DeserializeObject<ConfigurationData>(File.ReadAllText(FileName));
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


    public static class GlobalsExtensions
    {
        public static string EnsureDirectoryExist(this string thisValue)
        {
            if (!Directory.Exists(thisValue))
                Directory.CreateDirectory(thisValue);
            return thisValue;
        }
    }
}
