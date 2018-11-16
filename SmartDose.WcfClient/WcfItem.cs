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
                ConnectionName = WcfClientGlobals.ConnectionStringToConnectionName(_ConnectionString);
                if (string.IsNullOrEmpty(ConnectionStringUse))
                    ConnectionStringUse = ConnectionString;
            }
        }

        public string ConnectionName { get; private set; }

        public string ConnectionStringUse { get; set; }

        public bool Active { get; set; } = true;

        [JsonIgnore]
        public bool Build { get; set; } = true;

        public override string ToString()
            => $"{ConnectionString} [Active={Active}/Build={Build}]";
    }
}
