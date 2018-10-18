using System.Collections.Generic;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
    }
}
