using System;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Patient
    {
        public string ExternalPatientNumber { get; set; }
        public string RoomNumber { get; set; }
        public string BedNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
