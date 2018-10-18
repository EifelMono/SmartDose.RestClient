using System.ComponentModel;
using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// Contact Details Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ContactEx : Contact
    {
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

    }
}
