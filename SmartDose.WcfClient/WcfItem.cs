using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace SmartDose.WcfClient
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WcfItem
    {
        string _ConnectionString;
        public string ConnectionString
        {
            get => _ConnectionString; set
            {
                _ConnectionString = value;
                ConnectionName = SmartDoseWcfClientGlobals.ConnectionStringToConnectionName(_ConnectionString);
            }
        }

        public string ConnectionName { get; private set; }

        public string ConnectionStringUse { get; set; }

        public bool Active { get; set; }

        [JsonIgnore]
        public bool Build { get; set; }

        public override string ToString()
            => ConnectionString;
    }
}
