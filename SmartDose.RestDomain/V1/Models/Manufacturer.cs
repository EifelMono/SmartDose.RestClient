using Newtonsoft.Json;

namespace SmartDose.RestDomain.V1.Models
{
    public class Manufacturer
    {
        [JsonProperty("Identifier")]
        public string ManufacturerId { get; set; }

        public virtual ContactAddress Address { get; set; }

        public virtual string Comment { get; set; }

        public virtual string Name { get; set; }
    }
}
