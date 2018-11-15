using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.Core.Extensions
{
    public static class LogExtensions
    {
        private static readonly object s_logObject = new object(); 
        private static void WriteLine(string type, string message)
        {
            var timeStamp = DateTime.Now;
            Task.Run(() =>
            {
                lock (s_logObject)
                {
                    if (!File.Exists(SmartDoseCoreGlobals.Log.LogFileName))
                        File.AppendAllText(SmartDoseCoreGlobals.Log.LogFileName, $"Time;Info;Message\r\n");
                    File.AppendAllText(SmartDoseCoreGlobals.Log.LogFileName, $"{timeStamp};{type};{message}\r\n");
                }
            });
        }
        public static string LogError(this string thisValue)
        {
            WriteLine("Error", thisValue);
            return thisValue;
        }

        public static string LogInformation(this string thisValue)
        {
            WriteLine("Information", thisValue);
            return thisValue;
        }

        public static string LogWarning(this string thisValue)
        {
            WriteLine("Warning", thisValue);
            return thisValue;
        }

        public static Exception LogException(this Exception thisValue)
        {
            WriteLine("Critical", thisValue.ToString());
            return thisValue;
        }
    }
}
