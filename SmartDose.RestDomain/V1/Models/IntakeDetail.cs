using System.Collections.Generic;

namespace SmartDose.RestDomain.V1.Models
{
    public class IntakeDetail
    {
        public string IntakeDateTime { get; set; }

        public List<MedicationDetail> MedicationDetails { get; set; }
    }
}
