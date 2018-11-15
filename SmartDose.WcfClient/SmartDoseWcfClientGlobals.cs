using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

        public static (List<string> Copied, List<string> NotCopied) CopyWcfClientsToDirectory()
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

        public static bool ExtractCakeBuilds()
        {
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
                    }
                }
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            }
            return false;
        }
    }


}
