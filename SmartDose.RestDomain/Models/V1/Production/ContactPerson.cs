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
    public class ContactPerson
    {
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
    }
}
