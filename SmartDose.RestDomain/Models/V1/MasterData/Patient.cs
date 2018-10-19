using System;
using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.MasterData
#else
namespace SmartDose.RestDomain.Models.V1.MasterData
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Patient
    {
        public string ExternalPatientNumber { get; set; }
        public string RoomNumber { get; set; }
        public string BedNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
