﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Globals
{
    public static class LogExtensions
    {
        private static object LogObject = new object(); 
        private static void WriteLine(string type, string message)
        {
            var timeStamp = DateTime.Now;
            Task.Run(() =>
            {
                lock (LogObject)
                {
                    if (!File.Exists(AppGlobals.Log.LogFileName))
                        File.AppendAllText(AppGlobals.Log.LogFileName, $"Time;Info;Message\r\n");
                    File.AppendAllText(AppGlobals.Log.LogFileName, $"{timeStamp};{type};{message}\r\n");
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