using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SmartDose.RestDummy
{
    public static class RestDummyGlobals
    {
        public static string ReadFromResource(string name)
        {
            // why not 2 using!!!! ? => set this https://msdn.microsoft.com/library/ms182334.aspx
            Stream stream = null;
            try
            {
                stream = Assembly.GetAssembly(typeof(RestDummyGlobals)).GetManifestResourceStream(name);
                using (var reader = new StreamReader(stream))
                {
                    stream = null;
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null) stream.Dispose();
            }
        }
    }
}
