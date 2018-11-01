#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Pouch type enum
    /// </summary>
    public enum PouchType
    {
        FixEmpty = -2,
        FixNull = -1,
        /// <summary>
        /// Empty
        /// </summary>
        Empty,
        /// <summary>
        /// Cutting
        /// </summary>
        Cutting,
        /// <summary>
        /// Header
        /// </summary>
        Header,
        /// <summary>
        /// Pill
        /// </summary>
        Pill
    }
}
