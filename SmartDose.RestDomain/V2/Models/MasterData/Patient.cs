﻿using SmartDose.RestDomain.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V2.Models.MasterData
{
    /// <summary>
    /// Patient model
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
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

        /// <summary>
        /// Gets or sets the gender of the patient
        /// </summary>
        [EnumValidation(typeof(Gender), optional:true)]
        public string Gender { get; set; }

        /// <summary>
        /// Get or sets the birthday of the patient       
        /// </summary>
        [DateTimeValidation("yyyy-MM-dd", "DateOfBirth is required with yyyy-MM-dd format.", optional:true)]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the culture of the patient
        /// </summary>
        [CultureValidationAttribute("Culture (Combination of languagecode-regioncode) is required.")]
        public string Culture { get; set; }
    }
}