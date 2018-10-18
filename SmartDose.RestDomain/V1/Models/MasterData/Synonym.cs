using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Synonym
    {
        public string Identifier { get; set; }
        public float Price { get; set; }
        public int Content { get; set; }
        public string Description { get; set; }
    }
}
