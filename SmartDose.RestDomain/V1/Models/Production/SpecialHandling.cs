using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
