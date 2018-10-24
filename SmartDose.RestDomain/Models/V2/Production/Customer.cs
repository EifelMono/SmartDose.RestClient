using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.ContactEx" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Customer : ContactEx
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the desciption.
        /// </summary>
        /// <value>
        /// The desciption.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        /// <value>
        /// The customer code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerCode is required")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the name line.
        /// </summary>
        /// <value>
        /// The name line.
        /// </value>
        public string NameLine { get; set; }
        public override string ToString()
            => $"{CustomerCode} {Name}";
    }
}
