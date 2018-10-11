using Newtonsoft.Json;

namespace SmartDose.RestDomain.V1.Models
{
    public class OrderStatus
    {
        [JsonProperty("ExternalID")]
        public string ExternalId { get; set; }

        public string OrderId { get; set; }

        public string CreateDate { get; set; }

        public string DispenseState { get; set; }

        public string MachineNumber { get; set; }
    }
}
