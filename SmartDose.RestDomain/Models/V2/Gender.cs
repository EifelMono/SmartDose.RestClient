using System.Runtime.Serialization;


#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2
#else
namespace SmartDose.RestDomain.Models.V2
#endif
{
    /// <summary>
    /// 
    /// </summary>
    public enum Gender
    {
        FixEmpty = -2,
        FixNull = -1,
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
