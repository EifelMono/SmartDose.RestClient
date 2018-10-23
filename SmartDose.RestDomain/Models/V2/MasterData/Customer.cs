using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// The customer model
    /// </summary>
    /// <seealso cref="Contact" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Customer : Contact
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        /// <value>
        /// The customer code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the customer is required!")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Gets or sets the desciption.
        /// </summary>
        /// <value>
        /// The desciption.
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
            => $"{Name} ({CustomerCode})";
    }
}
