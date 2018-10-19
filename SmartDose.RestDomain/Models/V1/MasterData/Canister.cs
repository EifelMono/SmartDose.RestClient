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
