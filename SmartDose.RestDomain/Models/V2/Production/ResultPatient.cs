using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// 
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
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
