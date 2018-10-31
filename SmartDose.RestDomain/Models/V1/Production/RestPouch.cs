using System.Collections.Generic;
using System.ComponentModel;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
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

#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<RestPill> Pills { get; set; } = new List<RestPill>();

        public string Spindle { get; set; }
    }
}
