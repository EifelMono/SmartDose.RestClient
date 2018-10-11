namespace SmartDose.RestDomain.V1.Models
{
    public class Patient
    {
        public Gender Gender { get; set; }

        public string ExternalPatientNumber { get; set; }

        public string RoomNumber { get; set; }

        public string BedNumber { get; set; }

        public string WardName { get; set; }

        public string DateOfBirth { get; set; }

        public ContactPerson ContactPerson { get; set; }

        public ContactAddress ContactAddress { get; set; }
    }
}
