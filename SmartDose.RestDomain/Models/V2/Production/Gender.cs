using System.Runtime.Serialization;


#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// 
    /// </summary>
    public enum Gender
    {
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
