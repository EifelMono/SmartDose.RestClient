﻿using System;
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

        private static string s_dataRowaDirectory = null;
        public static string DataRowaDirectory => s_dataRowaDirectory
                                              ?? (s_dataRowaDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Rowa")
                                                    .EnsureDirectoryExist());
        private static string s_dataBinDirectory = null;
        public static string DataBinDirectory => s_dataBinDirectory
                                              ?? (s_dataBinDirectory = Path.Combine(DataRowaDirectory, "Bin", AppName)
                                                    .EnsureDirectoryExist());
        public static string DataBinObjectJsonDirectory(Type type)
        => Path.Combine(DataBinDirectory, type.FullName.Replace("SmartDose.RestDomainDev.", "").Replace(".", "\\")).EnsureDirectoryExist();
        private static string s_dataProtocolDirectory = null;

        public static string DataProtocolDirectory => s_dataProtocolDirectory
                                              ?? (s_dataProtocolDirectory = Path.Combine(DataRowaDirectory, "Protocol", AppName)
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
