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
    public class Manufacturer
    {
        public string Identifier { get; set; }
        public ContactAddress Address { get; set; } = new ContactAddress();
        public string Comment { get; set; }
        public string Name { get; set; }
    }
}
