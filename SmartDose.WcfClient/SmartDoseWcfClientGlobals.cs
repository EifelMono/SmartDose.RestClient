using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartDose.Core;
using SmartDose.Core.Extensions;

namespace SmartDose.WcfClient
{
    public static class SmartDoseWcfClientGlobals
    {
        public static string AssemblyFramework = "netstandard2.0";

        static string _WcfDataBinDirectory = null;
        public static string WcfDataBinDirectory
        {
            get
            {
                if (_WcfDataBinDirectory is null)
                {
                    var dataBinDirectory = SmartDoseCoreGlobals.DataBinDirectory;
                    dataBinDirectory = Path.Combine(Path.GetDirectoryName(dataBinDirectory), "SmartDose.RestClientApp");
                    _WcfDataBinDirectory = Path.Combine(dataBinDirectory, "WcfClients").EnsureDirectoryExist();
                }
                return _WcfDataBinDirectory;
            }
        }

        public static string BuildCakeFileName { get => Path.Combine(WcfDataBinDirectory, "build.cake"); }
        public static string BuildPs1FileName { get => Path.Combine(WcfDataBinDirectory, "build.ps1"); }

        public static string WcfClientsFileName { get => Path.Combine(WcfDataBinDirectory, "wcfclients"); }

        public static (List<string> Copied, List<string> NotCopied) CopyWcfClientsToCurrentDirectory()
        {
            var copyied = new List<string>();
            var notCopied = new List<string>();
            foreach (var file in FindWcfClients())
            {
                try
                {
                    File.Copy(file, Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(file)));
                    copyied.Add(file);
                }
                catch
                {
                    notCopied.Add(file);
                }
            }
            return (copyied, notCopied);
        }

        public static List<string> FindWcfClients()
        {
            var result = new List<string>();
            foreach (var file in Directory.GetFiles(WcfDataBinDirectory, "*.dll", SearchOption.AllDirectories))
            {
                if (!file.ToLower().Contains(@"\tools\"))
                    result.Add(file);
            }
            return result;
        }

        public static bool ExtractCakeBuildToWcfClientDirectory()
        {
            var count = 0;
            {
                Stream stream = null;
                try
                {
                    stream = Assembly.GetAssembly(typeof(SmartDoseWcfClientGlobals)).GetManifestResourceStream($"{typeof(SmartDoseWcfClientGlobals).Namespace}.Cake.build.cake");
                    using (var reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        stream = null;
                        File.WriteAllText(BuildCakeFileName, result);
                        count++;
                    }
                }
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            }
            {
                Stream stream = null;
                try
                {
                    stream = Assembly.GetAssembly(typeof(SmartDoseWcfClientGlobals)).GetManifestResourceStream($"{typeof(SmartDoseWcfClientGlobals).Namespace}.Cake.build.ps1");
                    using (var reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        stream = null;
                        File.WriteAllText(BuildPs1FileName, result);
                        count++;
                    }
                }
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            }
            return count == 2;
        }

        public static void BuildWcfClientsAssemblies(List<WcfItem> wcfItems)
        {
            var exe = Environment.GetCommandLineArgs()[0];
            var sb = new StringBuilder();
            foreach (var item in wcfItems.Where(w => w.Rebuild))
                sb.AppendLine(item.ConnectionString);
            File.WriteAllText(WcfClientsFileName, sb.ToString());
            Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"& './build.ps1' --target=wcfclients --startafterready=\"{Environment.GetCommandLineArgs()[0]}\"",
                WorkingDirectory = Path.GetDirectoryName(BuildPs1FileName),
            });
        }
}


}
