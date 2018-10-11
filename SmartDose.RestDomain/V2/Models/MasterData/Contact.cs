using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.MasterData
{
    /// <summary>
    /// Contact Details Model
    /// </summary>
    public class Contact
    {
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
        [StringLength(25, ErrorMessage = "PostalCode length is greater 25 characters")]
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

        /// <summary>
        /// Gets or sets the cellphone number.
        /// </summary>
        /// <value>
        /// The cellphone number.
        /// </value>
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the landline number.
        /// </summary>
        /// <value>
        /// The landline number.
        /// </value>
        public string LandlineNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}