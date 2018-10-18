using System;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CanisterStatus
    {
        public string CanisterId { get; set; } = string.Empty;
        public string MedicineId { get; set; } = string.Empty;
        public string Charge { get; set; } = string.Empty;
        public int AmountLeft { get; set; } = 0;
        public string LastRefill { get; set; } = DateTime.UtcNow.ToShortDateString();
        public string Position { get; set; } = string.Empty;
        public bool Active { get; set; } = false;
        public int MachineNumber { get; set; } = 0;
    }
}
