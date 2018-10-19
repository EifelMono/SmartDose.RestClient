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
    public class PrintDetail
    {
        public string Name { get; set; }
        public string IntakeAdvice { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Color { get; set; }
        public string FormType { get; set; }
        public string PillType { get; set; }
        public string GenericName { get; set; }
        public string AdditionalAdvice { get; set; }
        public string MedicationClass { get; set; }
        public string Language { get; set; }
    }
}
