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
    public static class WcfClientGlobals
    {
        public static string AssemblyFramework = "netstandard2.0";

        public static string ConnectionStringToConnectionName(string connectionString)
        {
            connectionString = connectionString ?? "";
            try
            {
                var result = connectionString.Split(new[] { "//" }, StringSplitOptions.None)[1].Replace(":", "_").Replace("/", "_");
                if (result.EndsWith("_"))
                    result = result.Substring(0, result.Length - 1);
                return result;
            }
            catch
            {
                return connectionString;
            }
        }

        static string _WcfDataBinDirectory = null;
        public static string WcfDataBinDirectory
        {
            get
            {
                if (_WcfDataBinDirectory is null)
                {
                    var dataBinDirectory = CoreGlobals.DataBinDirectory;
                    dataBinDirectory = Path.Combine(Path.GetDirectoryName(dataBinDirectory), "SmartDose.RestClientApp");
                    _WcfDataBinDirectory = Path.Combine(dataBinDirectory, "WcfClients").EnsureDirectoryExist();
                }
                return _WcfDataBinDirectory;
            }
        }

        public static string BuildCakeFileName { get => Path.Combine(WcfDataBinDirectory, "build.cake"); }
        public static string BuildPs1FileName { get => Path.Combine(WcfDataBinDirectory, "build.ps1"); }

        public static string WcfClientsFileName { get => Path.Combine(WcfDataBinDirectory, "wcfclients"); }

        public static bool ExtractCakeBuildToWcfClientDirectory()
        {
            var count = 0;
            {
                Stream stream = null;
                try
                {
                    stream = Assembly.GetAssembly(typeof(WcfClientGlobals)).GetManifestResourceStream($"{typeof(WcfClientGlobals).Namespace}.Cake.build.cake");
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
                    stream = Assembly.GetAssembly(typeof(WcfClientGlobals)).GetManifestResourceStream($"{typeof(WcfClientGlobals).Namespace}.Cake.build.ps1");
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
            // made with Cake
            /*
            C:\ProgramData\Rowa\Bin\SmartDose.RestClientApp\WcfClients
            echo net.tcp://localhost:10000/MasterData/
            remove last backslash 
            md localhost_10000_MasterData
            cd localhost_10000_MasterData
            dotnet new console
            dotnet add package dotnet-svcutil
            rename in csproj PackageReference=> DotNetCliToolReference 
            rename Framework => netstandard2.0
            dotnet restore
            dotnet svcutil net.tcp://localhost:9002/MasterData -n *,localhost_10000_MasterData
            Add (ExpandableObjectConverter) and more???? to classes in Reference.cs
            dotnet build
            cleanup
            */
            var exe = Environment.GetCommandLineArgs()[0];
            var sb = new StringBuilder();
            foreach (var item in wcfItems.Where(w => w.Build))
                sb.AppendLine(item.ConnectionString);
            File.WriteAllText(WcfClientsFileName, sb.ToString());
            Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"& './build.ps1' --target=wcfclients --startafterready=\"{Environment.GetCommandLineArgs()[0]}\"",
                WorkingDirectory = Path.GetDirectoryName(BuildPs1FileName),
            });
        }

        public static List<string> FindWcfClientAssemblies()
        {
            var result = new List<string>();
            foreach (var file in Directory.GetFiles(WcfDataBinDirectory, "*.dll", SearchOption.AllDirectories))
            {
                if (!file.ToLower().Contains(@"\tools\"))
                    result.Add(file);
            }
            return result;
        }

        public static (List<string> Copied, List<string> NotCopied) CopyWcfClientAssembliesToAppExecutionDirectory()
        {
            var copyied = new List<string>();
            var notCopied = new List<string>();
            foreach (var file in FindWcfClientAssemblies())
            {
                try
                {
                    File.Copy(file, Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(file)),true);
                    copyied.Add(file);
                }
                catch
                {
                    notCopied.Add(file);
                }
            }
            return (copyied, notCopied);
        }
}


}
