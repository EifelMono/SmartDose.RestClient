using Newtonsoft.Json;

namespace SmartDose.RestDomain.V1.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public string Fax { get; set; }

        public ContactAddress ContactAddress { get; set; }

        public ContactPerson ContactPerson { get; set; }

        public override string ToString()
           => $"Customer CustomerId={CustomerId} Name={Name}";
    }
}
