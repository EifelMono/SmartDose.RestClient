using System.Collections.Generic;

namespace SmartDose.RestDomain.V1.Models
{
    public class ExternalOrder
    {
        public string ExternalId { get; set; }

        public string Timestamp { get; set; }

        public Customer Customer { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public OrderState State { get; set; }
    }
}
