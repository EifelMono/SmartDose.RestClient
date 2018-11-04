using SmartDose.RestDomain.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;
using SmartDose.RestDomain.Converter;

#if RestDomainDev
using System.Drawing.Design;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// The pill model
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Pill
    {
        /// <summary>
        /// Gets or sets the medicine code.
        /// </summary>
        /// <value>
        /// The medicine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the medicine is required.")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid quantity. Must be a positive number with two decimal places")]
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the lot number.
        /// </summary>
        /// <value>
        /// The lot number.
        /// </value>
        public string LotNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        [DateTimeValidation("yyyy-MM-dd", "ExpiryDate is required with yyyy-MM-dd format.", optional:true)]
        public string ExpiryDate { get; set; }
#if RestDomainDev
        [TypeConverter(typeof(Date_yyyy_MM_dd_TypeConverter))]
        [Editor(typeof(DateTime_yyyy_MM_dd_Editor), typeof(UITypeEditor))]
#endif
        [JsonIgnore]
        public DateTime ExpiryDateAsType
        {
            get => NameAsTypeConverter.StringToDateTime_yyyy_MM_dd(ExpiryDate);
            set => ExpiryDate = NameAsTypeConverter.DateTimeToString_yyyy_MM_dd(value);
        }
    }
}
