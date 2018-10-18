using System.Collections.Generic;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RestPouch
    {
        public string PouchId { get; set; }

        public string PouchType { get; set; }

        public string PatienId { get; set; }

        public RestPill[] Pills { get; set; }

        public string Spindle { get; set; }
    }
}
