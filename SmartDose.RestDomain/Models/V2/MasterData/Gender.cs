#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Gender enumeration.
    /// </summary>
    public enum Gender
    {
        Emtpy = -2,
        Null = -1,
        /// <summary>
        /// the gender is not set, this is the default value used.
        /// </summary>
        Undefined,

        /// <summary>
        /// gender neutral or unimportend
        /// </summary>
        Undifferentiated,

        /// <summary>
        /// Set the gender to male
        /// </summary>
        Male,

        /// <summary>
        /// Set the gender to female
        /// </summary>
        Female,
    }
}
