using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartDose.RestDomain.V1.Models
{
    public class OrderResult
    {
        [JsonProperty("ExternalID")]
        public string ExternalId { get; set; }

        public string OrderId { get; set; }

        public string MachineNumber { get; set; }

        public string DispenseState { get; set; }

        public string CreateDate { get; set; }

        public string ProduceDate { get; set; }

        public List<Pouch> Pouches { get; set; }
    }
}
