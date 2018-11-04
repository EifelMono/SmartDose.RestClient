using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using System;


#if RestDomainDev
using System.Drawing.Design;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Order result by time
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />    
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif

    public class OrderResultByTime
    {
        /// <summary>
        /// Gets or sets the unique identifier of the external order.
        /// </summary>
        /// <value>
        /// The order code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the order is required.")]
        public string OrderCode { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the machine which executes the order
        /// </summary>
        /// <value>
        /// The machine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Machine code is required.")]
        public string MachineCode { get; set; }

        /// <summary>
        /// Gets or sets the dispense state.
        /// The progress of the production
        /// </summary>
        /// <value>
        /// The state of the dispense.
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
#if RestDomainDev
        [TypeConverter(typeof(Date_yyyy_MM_ddTHH_mm_ssZ_TypeConverter))]
        [Editor(typeof(DateTime_yyyy_MM_ddTHH_mm_ssZ_Editor), typeof(UITypeEditor))]
#endif
        [JsonIgnore]
        public DateTime CreationDateAsType
        {
            get => NameAsTypeConverter.StringToDateTime_yyyy_MM_ddTHH_mm_ssZ(CreationDate);
            set => CreationDate = NameAsTypeConverter.DateTimeToString_yyyy_MM_ddTHH_mm_ssZ(value);
        }

        /// <summary>
        /// Gets or sets the production date.
        /// </summary>
        /// <value>
        /// The production date.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Production Date is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string ProductionDate { get; set; }
#if RestDomainDev
        [TypeConverter(typeof(Date_yyyy_MM_ddTHH_mm_ssZ_TypeConverter))]
        [Editor(typeof(DateTime_yyyy_MM_ddTHH_mm_ssZ_Editor), typeof(UITypeEditor))]
#endif
        [JsonIgnore]
        public DateTime ProductionDateAsType
        {
            get => NameAsTypeConverter.StringToDateTime_yyyy_MM_ddTHH_mm_ssZ(ProductionDate);
            set => ProductionDate = NameAsTypeConverter.DateTimeToString_yyyy_MM_ddTHH_mm_ssZ(value);
        }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Order details are required.")]
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<ResultOrderDetail> ResultOrderDetails { get; set; } = new List<ResultOrderDetail>();
    }
}
