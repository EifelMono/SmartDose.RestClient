using System.Runtime.Serialization;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Dispense Status
    /// </summary>
    public enum DispenseStatus
    {
        /// <summary>
        /// "Unknown": No information about job state (failing connection etc.)
        /// </summary>
        DispenseInit,

        /// <summary>
        /// The dispense started
        /// </summary>
        DispenseStarted,

        /// <summary>
        /// The dispense finished
        /// </summary>
        DispenseFinished,

        /// <summary>
        /// The dispense canceled
        /// </summary>
        DispenseCanceled,

        /// <summary>
        /// The dispense initialize order not yet imported
        /// </summary>
        OrderNotYetImported,

        /// <summary>
        /// The order not found
        /// </summary>
        OrderNotFound,

    }
}
