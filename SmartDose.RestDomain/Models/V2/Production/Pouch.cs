using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;

#if RestDomainDev
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Pouch model.
    /// </summary>  
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif

    public class Pouch
    {
        /// <summary>
        /// Gets or sets the pouch code.
        /// </summary>
        /// <value>
        /// The pouch code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the pouch is required.")]
        public string PouchCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the pouch.
        /// </summary>
        /// <value>
        /// The type of the pouch.
        /// </value>
#if RestDomainDev
        [CategoryAsString]
#endif 
        [EnumValidation(typeof(PouchType))]
        public string PouchType { get; set; }
#if RestDomainDev
        [CategoryAsType]
#endif
        [JsonIgnore]
        public PouchType PouchTypeAsType
        {
            get => NameAsTypeConverter.StringToEnum<PouchType>(PouchType);
            set => PouchType = NameAsTypeConverter.EnumToString(value);
        }

        /// <summary>
        /// Gets or sets the patient code.
        /// </summary>
        /// <value>
        /// The patient code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatientCode is required.")]
        public string PatientCode { get; set; }

        /// <summary>
        /// Gets or sets the spindle code.
        /// </summary>
        /// <value>
        /// The spindle code.
        /// </value>
        public string SpindleCode { get; set; }

        /// <summary>
        /// Gets or sets the pills.
        /// </summary>
        /// <value>
        /// The pills.
        /// </value>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<Pill> Pills { get; set; } = new List<Pill>();

    }
}
