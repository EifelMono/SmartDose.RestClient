#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
    /// <summary>
    /// The gender.
    /// </summary>
    public enum Gender
    {
        FixEmpty = -2,
        FixNull = -1,
        /// <summary>
        /// The undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// The undifferentiated.
        /// </summary>
        Undifferentiated,

        /// <summary>
        /// The male.
        /// </summary>
        Male,

        /// <summary>
        /// The female.
        /// </summary>
        Female
    }
}
