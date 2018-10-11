using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.MasterData
{
    /// <summary>
    /// Destination facility model
    /// </summary>
    /// <seealso cref="SmartDose.MasterData.RESTv2.Models.Contact" />
    public class DestinationFacility : Contact
    {

        /// <summary>
        /// Gets or sets the destination facility code.
        /// </summary>
        /// <value>
        /// The destination facility code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the Destination Facility is required!")]
        public string DestinationFacilityCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the destination facility.
        /// </summary>
        /// <value>
        /// The description.
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
