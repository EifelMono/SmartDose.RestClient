using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ContactAddress
    {
        public string NameLine1 { get; set; }
        public string Addressline1 { get; set; }
        /// <summary>
        /// The addressline 1.
        /// </summary>
        public string Addressline2 { get; set; }

        [StringLength(25, ErrorMessage = "PostalCode length is greater 25 characters")]
        public string Postalcode { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

}
