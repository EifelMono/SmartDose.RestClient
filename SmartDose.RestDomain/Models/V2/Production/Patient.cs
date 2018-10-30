﻿using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using SmartDose.RestDomain.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Converters;
using System;

#if RestDomainDev
using System.Drawing.Design;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Patient Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.Contact" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif

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
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [DateTimeValidation("yyyy-MM-dd", "DateOfBirth is required with yyyy-MM-dd format.")]
        [JsonConverter(typeof(DateTime_yyyy_MM_dd_Converter))]
#if RestDomainDev
        // [DisplayName("DateOfBirth"), Editor(typeof(DateTime_yyyy_MM_dd_Editor), typeof(UITypeEditor))]
        [TypeConverter(typeof(Date_yyyy_MM_dd_TypeConverter))]
#endif
        public DateTime DateOfBirth { get; set; }

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
