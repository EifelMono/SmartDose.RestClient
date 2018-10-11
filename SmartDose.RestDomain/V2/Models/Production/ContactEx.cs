using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Contact Details Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTv2.Model.BaseData" />
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
