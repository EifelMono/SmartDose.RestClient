
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Representing the details of an order.
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class ResultOrderDetail 
    {
        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        public ResultPatient ResultPatient { get; set; }

        /// <summary>
        /// Gets or sets the intake details.
        /// </summary>
        /// <value>
        /// The intake details.
        /// </value>
        public List<ResultIntakeDetail> ResultIntakeDetails { get; set; }
    }
}
