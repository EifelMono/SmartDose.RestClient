using System.ComponentModel;
using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// Contact Details Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Contact
    {

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the landline number.
        /// </summary>
        /// <value>
        /// The landline number.
        /// </value>
        public string LandlineNumber { get; set; }

        /// <summary>
        /// Gets or sets the cellphone number.
        /// </summary>
        /// <value>
        /// The cellphone number.
        /// </value>
        public string CellphoneNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the addressline.
        /// </summary>
        /// <value>
        /// The addressline.
        /// </value>
        public string AddressLine { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
    }
}
