using SmartDose.RestDomain.Validation;
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
    /// Order Status
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif

    public class OrderStatus
    {
        /// <summary>
        /// Gets or sets the order code.
        /// </summary>
        /// <value>
        /// The order code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the external order is required.")]
        public string OrderCode { get; set; }

        /// <summary>
        /// Gets or sets the dispense status.
        /// </summary>
        /// <value>
        /// The dispense status.
        /// </value>
        [EnumValidation(typeof(DispenseStatus))]
        public string DispenseState { get; set; }
        [JsonIgnore]
        public DispenseStatus DispenseStateAsType
        {
            get => NameAsTypeConverter.StringToEnum<DispenseStatus>(DispenseState);
            set => DispenseState = NameAsTypeConverter.EnumToString(value);
        }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Creation Date is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string CreationDate { get; set; }
    }
}
