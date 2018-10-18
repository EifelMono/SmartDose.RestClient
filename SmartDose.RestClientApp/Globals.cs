using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartDose.RestClientApp
{
    public static class Globals
    {
        public static void Init()
        {
            Configuration.Load();
            if (!Configuration.FileExists)
                Configuration.Save();
        }

        public static string AppName => Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);

        private static string _DataDirectory = null;
        public static string DataDirectory => _DataDirectory
                                              ?? (_DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Rowa", "Bin", AppName)
                                                    .EnsureDirectoryExist());

        public static class Configuration
        {
   
            public static string FileName => Path.Combine(DataDirectory, $"{AppName}.json");

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
                }
            }

        }
    }


    public static class GlobalsExtensions
    {
        public static string EnsureDirectoryExist(this string directoryName)
        {
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            return directoryName;
        }
    }
}
