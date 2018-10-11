using SmartDose.RestDomain.Validation;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Order Status
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// Gets or sets the order code.
        /// </summary>
        /// <value>
        /// The order code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the external order is required.")]
        public string OrderCode { get; set; }

        /// <summary>
        /// Gets or sets the dispense status.
        /// </summary>
        /// <value>
        /// The dispense status.
        /// </value>
        [EnumValidation(typeof(DispenseStatus))]
        public string DispenseStatus { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Creation Date is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string CreationDate { get; set; }
    }
}
