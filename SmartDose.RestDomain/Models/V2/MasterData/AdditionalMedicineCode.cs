using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Additional Medicine Code
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class AdditionalMedicineCode
    {
        /// <summary>
        /// Gets or sets the additional medicine code.
        /// </summary>
        /// <value>
        /// The additional medicine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Additional code is required.")]
        public string AdditionalCode { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public int Content { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        public override string ToString()
            => $"{AdditionalCode}";

    }
}
