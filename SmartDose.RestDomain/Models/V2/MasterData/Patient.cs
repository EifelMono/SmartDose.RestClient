﻿using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using SmartDose.RestDomain.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SmartDose.RestDomain.Models.V2;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Patient model
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Patient
    {
        /// <summary>
        /// Gets or sets the patient code.
        /// </summary>
        /// <value>
        /// The patient code.
        /// </value>      
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique patient code is required.")]
        public string PatientCode { get; set; }

        /// <summary>
        /// Gets or sets the first name of the patient
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the patient
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the print name of the patient
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Print name is required.")]
        public string PrintName { get; set; }

        /// <summary>
        /// Gets or sets the address line of the patient
        /// </summary>
        public string AddressLine { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the patient
        /// </summary>
        [StringLength(25, ErrorMessage = "PostalCode length is greater 25 characters")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the cell phone number of the patient
        /// </summary>
        public string CellPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the landline number of the patient
        /// </summary>
        public string LandlineNumber { get; set; }

        /// <summary>
        /// Gets or sets the email of the patient
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the fax of the patient
        /// </summary>
        public string Fax { get; set; }

        [EnumValidation(typeof(Gender), optional: true)]
        public string Gender { get; set; }= NameAsTypeConverter.EnumToString(V2.Gender.Undefined);
        [JsonIgnore]
        public Gender GenderAsType
        {
            get => NameAsTypeConverter.StringToEnum<Gender>(Gender);
            set => Gender = NameAsTypeConverter.EnumToString(value);
        }

        /// <summary>
        /// Get or sets the birthday of the patient       
        /// </summary>
        [DateTimeValidation("yyyy-MM-dd", "DateOfBirth is required with yyyy-MM-dd format.")]
        public string DateOfBirth { get; set; }
#if RestDomainDev
        [TypeConverter(typeof(Date_yyyy_MM_dd_TypeConverter))]
#endif
        [JsonIgnore]
        public DateTime DateOfBirthAsType
        {
            get => NameAsTypeConverter.StringToDateTime_yyyy_MM_dd(DateOfBirth);
            set => DateOfBirth = NameAsTypeConverter.DateTimeToString_yyyy_MM_dd(value);
        }

        /// <summary>
        /// Gets or sets the culture of the patient
        /// </summary>
        [CultureValidation("Culture (Combination of languagecode-regioncode) is required.")]
        public string Culture { get; set; }
        [JsonIgnore]
        public CultureName CultureAsType
        {
            get => NameAsTypeConverter.StringToCultureName(Culture);
            set => Culture = NameAsTypeConverter.CultureNameToString(value);
        }


        public override string ToString()
          => $"{PatientCode} {FirstName} {LastName}";
    }
}
