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
    public class Patient
    {
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public Gender Gender { get; set; } = Gender.Null;

        /// <summary>
        /// Gets or sets the external patient number.
        /// </summary>
        [Required]
        public string ExternalPatientNumber { get; set; }

        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        public string RoomNumber { get; set; }

        /// <summary>
        /// Gets or sets the bed number.
        /// </summary>
        public string BedNumber { get; set; }

        /// <summary>
        /// Gets or sets the ward name.
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the contact person.
        /// </summary>
        public ContactPerson ContactPerson { get; set; } = new ContactPerson();

        /// <summary>
        /// Gets or sets the contact address.
        /// </summary>
        public ContactAddress ContactAddress { get; set; } = new ContactAddress();

        public override string ToString()
            => $"{ExternalPatientNumber}";
    }
}
