using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultPatient
    {
        /// <summary>
        /// Gets or sets the patient code.
        /// </summary>
        /// <value>
        /// The patient code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatientCode is required")]
        public string PatientCode { get; set; }
    }
}