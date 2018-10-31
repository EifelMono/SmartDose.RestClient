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
        Emtpy = -2,
        Null = -1,
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
