using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.MasterData
{
    /// <summary>
    /// Manufacturer model
    /// </summary>
    public class Manufacturer : Contact
    {
        /// <summary>
        /// Gets or sets the medicine code.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique code that identifies a manufacturer is required!")]
        public string ManufacturerCode { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
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