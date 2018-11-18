using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
using SmartDose.Core;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Medication Detail
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class MedicationDetail
    {
        /// <summary>
        /// Gets or sets the medicine code.
        /// </summary>
        /// <value>
        /// The medicine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique medicine code is required.")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity is required.")]
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the medication attributes.
        /// </summary>
        /// <value>
        /// The medication attributes.
        /// </value>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<AdditionalAttribute> MedicationAttributes { get; set; } = new List<AdditionalAttribute>();
    }
}
