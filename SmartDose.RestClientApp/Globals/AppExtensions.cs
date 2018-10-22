using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartDose.RestClientApp.Globals
{
    public static class AppExtensions
    {
        public static string ToJson(this object thisObject) => JsonConvert.SerializeObject(thisObject, Formatting.Indented);
    }
}
