using System.Collections.Generic;
using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class RestPouch
    {
        public string PouchId { get; set; }

        public string PouchType { get; set; }

        public string PatienId { get; set; }

        public RestPill[] Pills { get; set; }

        public string Spindle { get; set; }
    }
}
