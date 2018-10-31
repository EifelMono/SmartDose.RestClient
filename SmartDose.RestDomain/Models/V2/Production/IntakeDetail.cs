using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SmartDose.RestDomain.Validation;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Intake Detail
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class IntakeDetail
    {
        /// <summary>
        /// Gets or sets the intake date time.
        /// </summary>
        /// <value>
        /// The intake date time.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ss", "Intake Date Time requires this yyyy-MM-ddTHH:mm:ss format.")]
        public string IntakeDateTime { get; set; }

        /// <summary>
        /// Gets or sets the medication details.
        /// </summary>
        /// <value>
        /// The medication details.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Array of all medications for that intake time is required.")]
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<MedicationDetail> MedicationDetails { get; set; } = new List<MedicationDetail>();

        /// <summary>
        /// Gets or sets the intake detail attributes.
        /// </summary>
        /// <value>
        /// The intake detail attributes.
        /// </value>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<AdditionalAttribute> IntakeDetailAttributes { get; set; } = new List<AdditionalAttribute>();
        
    }
}
