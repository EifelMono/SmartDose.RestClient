using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// 
    /// </summary>
    public class AdditionalAttribute
    {        
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Key is required.")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Value is required.")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}