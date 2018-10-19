using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class DestinationFacility
    {
        /// <summary>
        /// Gets or sets the department code.
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Gets or sets the department name.
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the contact address.
        /// </summary>
        public ContactAddress ContactAddress { get; set; }

        /// <summary>
        /// Gets or sets the contact person.
        /// </summary>
        public ContactPerson ContactPerson { get; set; }
    }
}
