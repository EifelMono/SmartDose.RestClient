using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
    }
}
