using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Manufacturer model
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
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

        public override string ToString()
            => $"{ManufacturerCode}";
    }
}
