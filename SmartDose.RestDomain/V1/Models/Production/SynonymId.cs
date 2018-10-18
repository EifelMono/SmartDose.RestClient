using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SynonymId
    {
        public string Identifier { get; set; }
        public float Price { get; set; }
        public int Content { get; set; }
        public string Description { get; set; }
    }
}
