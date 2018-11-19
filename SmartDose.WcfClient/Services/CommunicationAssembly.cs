using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SmartDose.Core.Extensions;

namespace SmartDose.WcfClient.Services
{
    public class CommunicationAssembly : IDisposable
    {
        public Assembly Assembly { get; set; }
        public string AssemblyFilename { get; set; }
        public CommunicationAssembly(string assemblyFilename)
        {
            AssemblyFilename = assemblyFilename;
            if (!HasFileNameToLoad)
                return;
            try
            {
                IsLoaded = false;
                var filename = Path.Combine(
                    Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]),
                    Path.GetFileNameWithoutExtension(AssemblyFilename) + ".dll");
                if (File.Exists(AssemblyFilename))
                {
                    Assembly = Assembly.LoadFrom(assemblyFilename);
                    IsLoaded = true;
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }
        public void Dispose()
        {
            // cannot unload Assembly
        }

        public bool HasFileNameToLoad => !string.IsNullOrEmpty(AssemblyFilename);

        public bool IsLoaded { get; private set; } = true;
    }
}
