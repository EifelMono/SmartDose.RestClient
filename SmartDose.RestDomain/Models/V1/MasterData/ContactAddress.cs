using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.MasterData
#else
namespace SmartDose.RestDomain.Models.V1.MasterData
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class ContactAddress
    {
        public string NameLine1 { get; set; }
        public string Addressline1 { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

}
