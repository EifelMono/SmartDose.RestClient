using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartDose.Core.Extensions;

namespace SmartDose.Core
{
    public static class CoreGlobals
    {

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

    }
}
