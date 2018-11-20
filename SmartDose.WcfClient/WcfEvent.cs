using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.WcfClient
{
    public class WcfEvent
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string Name { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public object Value { get; set; }

        public override string ToString()
            => $"{TimeStamp.ToString("HH:mm:ss,fff")} {Name ?? ""}";
    }
}
