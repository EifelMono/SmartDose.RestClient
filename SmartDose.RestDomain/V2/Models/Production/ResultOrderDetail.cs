
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Representing the details of an order.
    /// </summary>
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
