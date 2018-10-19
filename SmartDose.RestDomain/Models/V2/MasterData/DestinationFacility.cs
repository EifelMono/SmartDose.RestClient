using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Destination facility model
    /// </summary>
    /// <seealso cref="SmartDose.MasterData.RESTv2.Models.Contact" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
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
