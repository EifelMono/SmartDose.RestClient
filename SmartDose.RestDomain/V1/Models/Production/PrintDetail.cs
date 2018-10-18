using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
    }
}
