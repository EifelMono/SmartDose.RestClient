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
                _ConnectionString = (value ??  "").Trim();
                if (string.IsNullOrEmpty(ConnectionName))
                    ConnectionName = WcfClientGlobals.ConnectionStringToConnectionName(_ConnectionString);
                if (string.IsNullOrEmpty(ConnectionStringUse))
                    ConnectionStringUse = ConnectionString;
            }
        }

        string _ConnectionName;
        public string ConnectionName
        {
            get => _ConnectionName;
            set => _ConnectionName = WcfClientGlobals.CheckConnectionName(value);
        }

        string _ConnectionStringUse;
        public string ConnectionStringUse
        {
            get => _ConnectionStringUse;
            set => _ConnectionStringUse = (value ?? "").Trim();
        }

        public bool Active { get; set; } = true;

        public bool Build { get; set; } = true;

        public override string ToString()
            => $"{ConnectionString} [Active={Active}/Build={Build}]";
    }
}
