using System.Collections.Generic;
using System.ComponentModel;

#if RestDomainDev
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
        public MedicationDetail[] MedicationDetails { get; set; }

        public override string ToString()
            => $"{IntakeDateTime}";
    }
}
