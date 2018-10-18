using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Manufacturer
    {
        public string Identifier { get; set; }
        public ContactAddress Address { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
    }
}
