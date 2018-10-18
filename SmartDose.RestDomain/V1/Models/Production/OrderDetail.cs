using System.Collections.Generic;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OrderDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetail"/> class.
        /// </summary>
        public OrderDetail()
        {

        }

        /// <summary>
        /// Gets or sets the pharmacy.
        /// </summary>
        public Customer Pharmacy { get; set; }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the destination facility.
        /// </summary>
        public DestinationFacility DestinationFacility { get; set; }

        /// <summary>
        /// Gets or sets the external detail print info 1.
        /// </summary>
        public string ExternalDetailPrintInfo1 { get; set; }

        /// <summary>
        /// Gets or sets the external detail print info 2.
        /// </summary>
        public string ExternalDetailPrintInfo2 { get; set; }

        /// <summary>
        /// Gets or sets the intake details.
        /// </summary>
        public IntakeDetail[] IntakeDetails { get; set; }
    }
}
