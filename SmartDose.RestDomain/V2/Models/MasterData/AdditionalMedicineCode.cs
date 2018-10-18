using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Models.MasterData
{
    /// <summary>
    /// Additional Medicine Code
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
    }
}
