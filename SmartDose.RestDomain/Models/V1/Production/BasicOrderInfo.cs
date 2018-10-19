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
    public class BasicOrderInfo
    {
        public string ExternalID { get; set; }

        public string OrderId { get; set; }

        public string CreateDate { get; set; }

        public string DispenseState { get; set; }

        public string MachineNumber { get; set; }
    }
}
