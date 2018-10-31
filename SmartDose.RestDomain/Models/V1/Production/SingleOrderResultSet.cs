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
    public class SingleOrderResultSet
    {
        public string ExternalID { get; set; }

        public string OrderId { get; set; }

        public string MachineNumber { get; set; }

        public string DispenseState { get; set; }

        public string CreateDate { get; set; }

        public string ProduceDate { get; set; }
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<RestPouch> Pouches { get; set; } = new List<RestPouch>();
    }
}
