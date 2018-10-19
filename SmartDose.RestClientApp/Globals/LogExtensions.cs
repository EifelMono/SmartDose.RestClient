using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SmartDose.RestClientApp.Globals
{
    public static class LogExtensions
    {
        public static string LogError(this string thisValue)
        {
            AppGlobals.Log.Logger.LogError(thisValue);
            return thisValue;
        }

        public static string LogInformation(this string thisValue)
        {
            AppGlobals.Log.Logger.LogInformation(thisValue);
            return thisValue;
        }

        public static string LogWarning(this string thisValue)
        {
            AppGlobals.Log.Logger.LogWarning(thisValue);
            return thisValue;
        }

        public static void LogException(this Exception thisValue)
        {
            AppGlobals.Log.Logger.LogCritical(thisValue.Message);
        }
    }
}
