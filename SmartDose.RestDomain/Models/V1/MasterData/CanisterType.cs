#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.MasterData
#else
namespace SmartDose.RestDomain.Models.V1.MasterData
#endif
{
    public enum CanisterType
    {
        Emtpy = -2,
        Null = -1,
        Unknown = 0,
        Transparent = 1,
        Opaque = 2,
    }
}
