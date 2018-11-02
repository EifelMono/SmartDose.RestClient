using System;
using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1
#else
namespace SmartDose.RestDomain.Models.V1
#endif
{
    /// <summary>
    /// The medicine container.
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class ResultSet
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Code} {Detail}";
        }
    }
}
