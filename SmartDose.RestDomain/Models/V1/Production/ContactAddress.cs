using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
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
