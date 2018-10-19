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
    public class ContactPerson
    {
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
    }
}
