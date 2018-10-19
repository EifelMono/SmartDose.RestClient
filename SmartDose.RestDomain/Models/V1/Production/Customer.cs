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
    public class Customer
    {
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        public long CustomerId { get; set; }

        public string Identifier { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        public string Fax { get; set; }

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
