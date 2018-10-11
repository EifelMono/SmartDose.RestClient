using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.MasterData
{
    /// <summary>
    /// The customer model
    /// </summary>
    /// <seealso cref="Contact" />
    public class Customer : Contact
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        /// <value>
        /// The customer code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the customer is required!")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Gets or sets the desciption.
        /// </summary>
        /// <value>
        /// The desciption.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name line.
        /// </summary>
        /// <value>
        /// The name line.
        /// </value>
        public string NameLine { get; set; }
    }
}