using System.Collections.Generic;

namespace SmartDose.RestDomain.V1.Models
{
    public class OrderDetail
    {
        public Customer Pharmacy { get; set; }

        public Patient Patient { get; set; }

        public DestinationFacility DestinationFacility { get; set; }

        public string ExternalDetailPrintInfo1 { get; set; }

        public string ExternalDetailPrintInfo2 { get; set; }

        public List<IntakeDetail> IntakeDetails { get; set; }
    }
}
