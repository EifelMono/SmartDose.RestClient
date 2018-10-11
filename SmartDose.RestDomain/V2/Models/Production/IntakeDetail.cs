﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmartDose.RestDomain.Validation;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Intake Detail
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTv2.Model.BaseData" />
    public class IntakeDetail
    {
        /// <summary>
        /// Gets or sets the intake date time.
        /// </summary>
        /// <value>
        /// The intake date time.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ss", "Intake Date Time requires this yyyy-MM-ddTHH:mm:ss format.")]
        public string IntakeDateTime { get; set; }

        /// <summary>
        /// Gets or sets the medication details.
        /// </summary>
        /// <value>
        /// The medication details.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Array of all medications for that intake time is required.")]
        public List<MedicationDetail> MedicationDetails { get; set; }

        /// <summary>
        /// Gets or sets the intake detail attributes.
        /// </summary>
        /// <value>
        /// The intake detail attributes.
        /// </value>
        public List<AdditionalAttribute> IntakeDetailAttributes { get; set; }
        
    }
}
