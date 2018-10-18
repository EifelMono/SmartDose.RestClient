using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SmartDose.RestDomain.Validation;

namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// Patient Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.Contact" />
    [TypeConverter(typeof(ExpandableObjectConverter))]

    public class Patient : Contact
    {

        /// <summary>
        /// Gets or sets the patient code.
        /// </summary>
        /// <value>
        /// The patient code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatientCode is required")]
        public string PatientCode { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the print.
        /// </summary>
        /// <value>
        /// The name of the print.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "PrintName is required")]
        public string PrintName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [EnumValidation(typeof(Gender), optional:true)]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [DateTimeValidation("yyyy-MM-dd", "DateOfBirth is required with yyyy-MM-dd format.", optional:true)]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>.
        /// <value>
        /// The culture.
        /// </value>
        [CultureValidation("Culture (Combination of languagecode-regioncode) is required.")]
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the patient attributes.
        /// </summary>
        /// <value>
        /// The patient attributes.
        /// </value>
        public AdditionalAttribute[] PatientAttributes { get; set; }
    }
}
