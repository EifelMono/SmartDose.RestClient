using System.Collections.Generic;
using System.ComponentModel;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class IntakeDetail
    {
        public IntakeDetail()
        {

        }

        /// <summary>
        /// Gets or sets the intake date time.
        /// </summary>
        public string IntakeDateTime { get; set; }

        /// <summary>
        /// Gets or sets the medication details.
        /// </summary>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<MedicationDetail> MedicationDetails { get; set; } = new List<MedicationDetail>();

        public override string ToString()
            => $"{IntakeDateTime}";
    }
}
