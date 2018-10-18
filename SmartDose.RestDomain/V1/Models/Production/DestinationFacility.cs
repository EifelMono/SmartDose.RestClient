using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
