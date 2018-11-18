using System.Collections.Generic;
using System.ComponentModel;

#if RestDomainDev
using SmartDose.Core;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class RestOrderDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetail"/> class.
        /// </summary>
        public RestOrderDetail()
        {

        }

        /// <summary>
        /// Gets or sets the pharmacy.
        /// </summary>
        public RestExternalCustomer Pharmacy { get; set; } = new RestExternalCustomer();

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        public Patient Patient { get; set; } = new Patient();

        /// <summary>
        /// Gets or sets the destination facility.
        /// </summary>
        public DestinationFacility DestinationFacility { get; set; } = new DestinationFacility();

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
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif 
        public List<IntakeDetail> IntakeDetails { get; set; } = new List<IntakeDetail>();
    }
}
