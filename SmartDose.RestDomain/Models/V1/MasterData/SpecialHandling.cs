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
    public class SpecialHandling
    {
        public bool Narcotic { get; set; }
        public bool SeparatePouch { get; set; }
        public bool RoboticHandling { get; set; }
        public bool NeedsCooling { get; set; }
        public bool Splittable { get; set; }
        public int MaxAmountPerPouch { get; set; }
    }
}
