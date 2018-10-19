using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Pharmaceutical Attributes
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class PharmaceuticalAttributes
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is a narcotic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if narcotic; otherwise, <c>false</c>.
        /// </value>
        public bool Narcotic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a cytostatic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cytostatic; otherwise, <c>false</c>.
        /// </value>
        public bool Cytostatic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it needs cooling.
        /// </summary>
        /// <value>
        ///   <c>true</c> if needs cooling; otherwise, <c>false</c>.
        /// </value>
        public bool NeedsCooling { get; set; }
    }
}
