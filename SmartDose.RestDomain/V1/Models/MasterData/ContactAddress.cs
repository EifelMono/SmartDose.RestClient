using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ContactAddress
    {
        public string NameLine1 { get; set; }
        public string Addressline1 { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

}
