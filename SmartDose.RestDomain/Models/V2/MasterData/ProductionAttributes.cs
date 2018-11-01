
using System.ComponentModel;
using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using SmartDose.RestDomain.Validation;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.MasterData
#else
namespace SmartDose.RestDomain.Models.V2.MasterData
#endif
{
    /// <summary>
    /// Production Attributes
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
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
#if RestDomainDev
        [CategoryAsString]
#endif
        [EnumValidation(typeof(FillingMode), optional: true)]
        public string FillingMode { get; set; }
#if RestDomainDev
        [CategoryAsType]
#endif
        [JsonIgnore]
        public FillingMode FillingModeAsType
        {
            get => NameAsStringConvert.StringToEnum<FillingMode>(FillingMode);
            set => FillingMode = NameAsStringConvert.EnumToString(value);
        }
    }
}
