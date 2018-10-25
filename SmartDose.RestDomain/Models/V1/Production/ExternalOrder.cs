using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class ExternalOrder
    {
        public ExternalOrder()
        {

        }

        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        [Required(ErrorMessage = "An ExternalId is required")]
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public OrderState State { get; set; }

        [JsonIgnore]
        public List<string> UsedMedicines
        {
            get => OrderDetails == null
                    ? new List<string>()
                    : OrderDetails.Where(od => od != null)
                        .SelectMany(od => od.IntakeDetails)
                            .Where(id => id != null)
                                .SelectMany(id => id.MedicationDetails)
                                    .Where(md => md != null && md.MedicineId != null)
                                        .Select(md => md.MedicineId)
                                            .Distinct().ToList();
        }
    }
}
