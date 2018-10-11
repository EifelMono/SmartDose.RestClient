using SmartDose.RestDomain.Validation;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// The pill model
    /// </summary>
    public class Pill
    {
        /// <summary>
        /// Gets or sets the medicine code.
        /// </summary>
        /// <value>
        /// The medicine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the medicine is required.")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid quantity. Must be a positive number with two decimal places")]
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the lot number.
        /// </summary>
        /// <value>
        /// The lot number.
        /// </value>
        public string LotNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        [DateTimeValidation("yyyy-MM-dd", "ExpiryDate is required with yyyy-MM-dd format.", optional:true)]
        public string ExpiryDate { get; set; }
    }
}
