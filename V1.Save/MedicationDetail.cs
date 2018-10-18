namespace SmartDose.RestDomain.V1.Models
{
    public class MedicationDetail
    {
        public string MedicineId { get; set; }

        public float Count { get; set; }

        public string IntakeAdvice { get; set; }

        public string Physician { get; set; }

        public string PhysicianComment { get; set; }

        public string PrescribedMedicine { get; set; }
    }
}
