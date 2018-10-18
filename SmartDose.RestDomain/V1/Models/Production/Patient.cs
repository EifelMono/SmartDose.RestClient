using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Patient
    {
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public Gender Gender { get; set; }

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
        public ContactPerson ContactPerson { get; set; }

        /// <summary>
        /// Gets or sets the contact address.
        /// </summary>
        public ContactAddress ContactAddress { get; set; }
    }
}
