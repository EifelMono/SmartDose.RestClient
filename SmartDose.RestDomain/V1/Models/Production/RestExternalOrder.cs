using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
