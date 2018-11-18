using System;
using System.Collections.Generic;
using System.ComponentModel;
using SmartDose.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartDose.RestDomainDev.PropertyEditorThings;
using SmartDose.WcfClient;

namespace SmartDose.RestClientApp.Globals
{
    public class ConfigurationData
    {
        public string UrlV1 { get; set; }

        public string UrlV2 { get; set; }
        public TimeSpan UrlTimeSpan { get; set; }

        
        [TypeConverter(typeof(ListConverter))]
        public List<WcfItem> WcfClients { get; set; } = new List<WcfItem>();

        public TimeSpan WcfTimeSpan { get; set; }
    }
}
