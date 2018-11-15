using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SmartDose.WcfClient
{
    public class WcfItem
    {
        public string ConnectionString { get; set; }

        public string ConnectionName { get;  }

        public string ConnectToConnectionString { get; set; }

        public bool Active { get; set; }

        [JsonIgnore]
        public bool Rebuild { get; set; }
    }
}
