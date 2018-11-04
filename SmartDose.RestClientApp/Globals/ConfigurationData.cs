using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Globals
{
    public class ConfigurationData
    {
        public string UrlV1 { get; set; } = "http://localhost:6040/SmartDose";
        // http://localhost:6040/SmartDose/Customers

        public string UrlV2 { get; set; } = "http://localhost:56040/SmartDose/V2.0";

        // http://localhost:56040/SmartDose/V2.0/MasterData/Customers
        // http://localhost:56040/SmartDose/V2.0/swagger/docs/v2

        public TimeSpan UrlTimeSpan { get; set; } = TimeSpan.FromSeconds(100);
    }
}
