using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Globals
{
    public class ConfigurationData
    {
        public string UrlRestV1 { get; set; } = "http://localhost:6040/SmartDose";
        // http://localhost:56040/SmartDose/Customers

        public string UrlRestV2 { get; set; } = "http://localhost:56040/SmartDose/V2.0";

        // http://localhost:56040/SmartDose/V2.0/MasterData/Customers
        // http://localhost:56040/SmartDose/V2.0/swagger/docs/v2
    }
}
