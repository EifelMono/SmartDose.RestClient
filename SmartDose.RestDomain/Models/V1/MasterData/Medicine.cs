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
    public class Medicine
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public Synonym[] SynonymIds { get; set; }
        public PrintDetail[] PrintDetails { get; set; }
        public MedicinePicture[] MedicinePictures { get; set; }
        public SpecialHandling SpecialHandling { get; set; }
        public bool TrayFillOnly { get; set; }
    }
}
