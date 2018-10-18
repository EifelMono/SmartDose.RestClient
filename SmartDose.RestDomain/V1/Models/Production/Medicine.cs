using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Medicine
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public SynonymId[] SynonymIds { get; set; }
        public PrintDetail[] PrintDetails { get; set; }
        public MedicinePicture[] MedicinePictures { get; set; }
        public SpecialHandling SpecialHandling { get; set; }
    }
}
