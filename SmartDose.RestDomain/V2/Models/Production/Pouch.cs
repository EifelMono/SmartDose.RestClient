using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Pouch model.
    /// </summary>  
    public class Pouch
    {
        /// <summary>
        /// Gets or sets the pouch code.
        /// </summary>
        /// <value>
        /// The pouch code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the pouch is required.")]
        public string PouchCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the pouch.
        /// </summary>
        /// <value>
        /// The type of the pouch.
        /// </value>
        [EnumValidation(typeof(PouchType))]
        public string PouchType { get; set; }

        /// <summary>
        /// Gets or sets the patient code.
        /// </summary>
        /// <value>
        /// The patient code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatientCode is required.")]
        public string PatientCode { get; set; }

        /// <summary>
        /// Gets or sets the spindle code.
        /// </summary>
        /// <value>
        /// The spindle code.
        /// </value>
        public string SpindleCode { get; set; }

        /// <summary>
        /// Gets or sets the pills.
        /// </summary>
        /// <value>
        /// The pills.
        /// </value>
        public List<Pill> Pills { get; set; }

    }
}
