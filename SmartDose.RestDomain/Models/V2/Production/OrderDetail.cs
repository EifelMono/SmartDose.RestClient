using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Order Detail Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Browsable(true)]
#endif
    public class OrderDetail 
    {
        /// <summary>
        /// Gets or sets the pharmacy.
        /// </summary>
        /// <value>
        /// The pharmacy.
        /// </value>
        public Pharmacy Pharmacy { get; set; }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        [Required(ErrorMessage = "Patient is required")]
        public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the destination facility.
        /// </summary>
        /// <value>
        /// The destination facility.
        /// </value>
        public DestinationFacility DestinationFacility { get; set; }

        /// <summary>
        /// Gets or sets the intake details.
        /// </summary>
        /// <value>
        /// The intake details.
        /// </value>
        [Required(ErrorMessage = "IntakeDetails are required")]
        public IntakeDetail[] IntakeDetails { get; set; }

        /// <summary>
        /// Gets or sets the order details attributes.
        /// </summary>
        /// <value>
        /// The order details attributes.
        /// </value>
        public AdditionalAttribute[] OrderDetailsAttributes { get; set; }

    }
}
