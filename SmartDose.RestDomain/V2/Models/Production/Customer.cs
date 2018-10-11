using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTv2.Model.ContactEx" />
    public class Customer : ContactEx
    {

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
        /// Gets or sets the customer code.
        /// </summary>
        /// <value>
        /// The customer code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerCode is required")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the name line.
        /// </summary>
        /// <value>
        /// The name line.
        /// </value>
        public string NameLine { get; set; }

    }
}
