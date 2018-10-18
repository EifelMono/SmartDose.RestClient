using Newtonsoft.Json;

namespace SmartDose.RestDomain.V1.Models
{
    public class ContactAddress
    {
        [JsonProperty("Addressline1")]
        public string AddressLine1 { get; set; }

  
        public string City { get; set; }

        public string Country { get; set; }

        public string NameLine1 { get; set; }

        public string Postalcode { get; set; }
       
        public string State { get; set; }
    }
}
