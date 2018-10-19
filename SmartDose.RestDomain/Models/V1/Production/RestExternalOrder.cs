using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class RestExternalOrder
    {
        public RestExternalOrder()
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
        // ? [DateTimeValidation("yyyy-MM-ddTHH:mm:ss", "Timestamp requires this format yyyy-MM-ddTHH:mm:ss")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        public RestExternalCustomer Customer { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        public RestOrderDetail[] OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public OrderState State { get; set; }
    }
}
