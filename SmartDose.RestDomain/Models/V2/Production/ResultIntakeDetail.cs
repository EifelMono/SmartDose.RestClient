using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Represents the details of one intake time.
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif

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
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<ResultMedicationDetail> ResultMedicationDetails { get; set; } = new List<ResultMedicationDetail>();

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
