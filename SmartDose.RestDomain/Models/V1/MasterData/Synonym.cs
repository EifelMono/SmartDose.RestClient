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
    public class Synonym
    {
        public string Identifier { get; set; }
        public float Price { get; set; }
        public int Content { get; set; }
        public string Description { get; set; }
    }
}
