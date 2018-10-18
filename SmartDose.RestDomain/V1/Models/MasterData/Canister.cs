using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Canister
    {
        public long Id { get; set; }
        public string CanisterId { get; set; }
        public string RfId { get; set; }
        public bool Largecanister { get; set; }
        public string RotorId { get; set; }
        public CanisterType CanisterType { get; set; } = CanisterType.Unknown;
    }
}
