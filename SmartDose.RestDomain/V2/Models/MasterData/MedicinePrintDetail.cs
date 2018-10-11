using SmartDose.RestDomain.Validation;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Model.MasterData
{
    /// <summary>
    /// Medicine Print Details
    /// </summary>
    public class MedicinePrintDetail
    {
        /// <summary>
        /// Gets or sets the print detail code.
        /// </summary>
        /// <value>
        /// The print detail code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Print detail code is required.")]
        public string PrintDetailCode { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        [CultureValidation("Culture (Combination of languagecode-regioncode) is required.")]
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the name of the print.
        /// </summary>
        /// <value>
        /// The name of the print.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Print name is required.")]
        public string PrintName { get; set; }

        /// <summary>
        /// Gets or sets the name of the generic print.
        /// </summary>
        /// <value>
        /// The name of the generic print.
        /// </value>
        public string GenericPrintName { get; set; }

        /// <summary>
        /// Gets or sets the intake advice.
        /// </summary>
        /// <value>
        /// The intake advice.
        /// </value>
        public string IntakeAdvice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the type of the form.
        /// </summary>
        /// <value>
        /// The type of the form.
        /// </value>
        public string FormType { get; set; }

        /// <summary>
        /// Gets or sets the type of the pill.
        /// </summary>
        /// <value>
        /// The type of the pill.
        /// </value>
        public string PillType { get; set; }

        /// <summary>
        /// Gets or sets the additional advice.
        /// </summary>
        /// <value>
        /// The additional advice.
        /// </value>
        public string AdditionalAdvice { get; set; }

        /// <summary>
        /// Gets or sets the medicine class.
        /// </summary>
        /// <value>
        /// The medicine class.
        /// </value>
        public string MedicineClass { get; set; }
    }
}