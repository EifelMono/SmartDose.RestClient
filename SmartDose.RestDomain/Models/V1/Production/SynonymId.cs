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
    public class SynonymId
    {
        public string Identifier { get; set; }
        public float Price { get; set; }
        public int Content { get; set; }
        public string Description { get; set; }
    }
}
