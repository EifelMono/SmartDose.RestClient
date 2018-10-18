
using System.ComponentModel;
using SmartDose.RestDomain.Validation;

namespace SmartDose.RestDomain.V2.Models.MasterData
{
    /// <summary>
    /// Production Attributes
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProductionAttributes
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is splittable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if splittable; otherwise, <c>false</c>.
        /// </value>
        public bool Splittable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is print only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if print only; otherwise, <c>false</c>.
        /// </value>
        public bool PrintOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is trayfill only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [trayfill only]; otherwise, <c>false</c>.
        /// </value>
        public bool TrayfillOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is pouchable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if pouchable; otherwise, <c>false</c>.
        /// </value>
        public bool Pouchable { get; set; }

        /// <summary>
        /// Gets or sets the filling mode.
        /// </summary>
        /// <value>
        /// The filling mode.
        /// </value>
        [EnumValidation(typeof(FillingMode), optional:true)]
        public string FillingMode { get; set; }
    }
}
