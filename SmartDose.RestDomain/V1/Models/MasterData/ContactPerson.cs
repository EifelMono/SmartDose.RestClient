using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ContactPerson
    {
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
    }
}
