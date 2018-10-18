using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Models.MasterData
{
    /// <summary>
    /// Pharmacy model
    /// </summary>
    /// <seealso cref="SmartDose.MasterData.RESTv2.Models.Contact" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Pharmacy : Contact
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        /// <value>
        /// The customer code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the Pharmacy is required.")]
        public string PharmacyCode { get; set; }

        /// <summary>
        /// Gets or sets the desciption.
        /// </summary>
        /// <value>
        /// The desciption.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the nameline.
        /// </summary>
        /// <value>
        /// The nameline.
        /// </value>
        public string NameLine { get; set; }
    }
}
