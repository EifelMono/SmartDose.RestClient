using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Medication Detail
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTv2.Model.BaseData" />
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
        public List<AdditionalAttribute> MedicationAttributes { get; set; }
    }
}