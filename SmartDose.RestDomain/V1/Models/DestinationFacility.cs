namespace SmartDose.RestDomain.V1.Models
{
    public class DestinationFacility
    {
        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string CustomerId { get; set; }

        public string Name { get; set; }

        public ContactAddress ContactAddress { get; set; }

        public ContactPerson ContactPerson { get; set; }
    }
}
