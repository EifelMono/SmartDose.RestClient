using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// Represents the details of one intake time.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]

    public class ResultIntakeDetail
    {
        /// <summary>
        /// Gets or sets the intake date time.
        /// </summary>
        /// <value>
        /// The intake date time.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddThh:mm:ss", "IntakeDateTime requires this format yyyy-MM-ddThh:mm:ss")]
        public string IntakeDateTime { get; set; }

        /// <summary>
        /// Gets or sets the medication details.
        /// </summary>
        /// <value>
        /// The medication details.
        /// </value>
        [Required(ErrorMessage = "ResultMedicationDetails is required")]
        public ResultMedicationDetail[] ResultMedicationDetails { get; set; }

        /// <summary>
        /// Gets or sets the pouches.
        /// </summary>
        /// <value>
        /// The pouches.
        /// </value>
        [Required(ErrorMessage = "Pouches is required")]
        public List<Pouch> Pouches { get; set; }
    }
}
