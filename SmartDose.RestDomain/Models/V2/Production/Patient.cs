using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using SmartDose.RestDomain.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

#if RestDomainDev
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
        /// Gets or sets the gender of the patient
        /// </summary>
        [EnumValidation(typeof(Gender), optional: true)]
#if RestDomainDev
        [CategoryAsString]
#endif
        public string Gender { get; set; }

        [JsonIgnore]
#if RestDomainDev
        [CategoryAsType]
#endif
        public Gender GenderAsType
        {
            get => NameAsStringConvert.StringToEnum<Gender>(Gender);
            set => Gender = NameAsStringConvert.EnumToString(value);
        }

        /// <summary>
        /// Get or sets the birthday of the patient       
        /// </summary>
        [DateTimeValidation("yyyy-MM-dd", "DateOfBirth is required with yyyy-MM-dd format.")]
#if RestDomainDev
        [CategoryAsString]
#endif
        public string DateOfBirth { get; set; }

        [JsonIgnore]
#if RestDomainDev
        [TypeConverter(typeof(Date_yyyy_MM_dd_TypeConverter))]
        [NotifyParentProperty(true)]
        [CategoryAsType]
#endif
        public DateTime DateOfBirthAsType
        {
            get => NameAsStringConvert.StringToDateTime_yyyy_MM_dd(DateOfBirth);
            set => DateOfBirth = NameAsStringConvert.DateTimeToString_yyyy_MM_dd(value);
        }


        /// <summary>
        /// Gets or sets the culture.
        /// </summary>.
        /// <value>
        /// The culture.
        /// </value>
        [CultureValidation("Culture (Combination of languagecode-regioncode) is required.")]
#if RestDomainDev
        [TypeConverter(typeof(CultureTypeConverter))]
#endif
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the patient attributes.
        /// </summary>
        /// <value>
        /// The patient attributes.
        /// </value>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<AdditionalAttribute> PatientAttributes { get; set; } = new List<AdditionalAttribute>();
    }
}
