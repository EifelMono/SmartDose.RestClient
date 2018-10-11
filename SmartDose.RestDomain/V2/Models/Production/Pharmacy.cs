using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTv2.Model.Contact" />
    public class Pharmacy : Contact
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        /// <value>
        /// The customer code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "PharmacyCode is required")]
        public string PharmacyCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the desciption.
        /// </summary>
        /// <value>
        /// The desciption.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name first line.
        /// </summary>
        /// <value>
        /// The name first line.
        /// </value>
        public string NameFirstLine { get; set; }

        /// <summary>
        /// Gets or sets the name second line.
        /// </summary>
        /// <value>
        /// The name second line.
        /// </value>
        public string NameSecondLine { get; set; }

    }
}
