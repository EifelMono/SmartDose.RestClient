using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using SmartDose.RestDomain.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
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
    /// Multidose Order Model
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Order
    {
        /// <summary>
        /// Gets or sets the order code.
        /// </summary>
        /// <value>
        /// The order code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unique identifier of the order is required")]
        public string OrderCode { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>

#if RestDomainDev
        [CategoryAsString]
#endif
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Timestamp is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string Timestamp { get; set; } = NameAsTypeConverter.DateTimeToString_yyyy_MM_ddTHH_mm_ssZ(DateTime.Now);
#if RestDomainDev
        [CategoryAsType]
        [TypeConverter(typeof(Date_yyyy_MM_ddTHH_mm_ssZ_TypeConverter))]
        [Editor(typeof(DateTime_yyyy_MM_ddTHH_mm_ssZ_Editor), typeof(UITypeEditor))]
#endif
        [JsonIgnore]
        public DateTime TimestampAsType
        {
            get => NameAsTypeConverter.StringToDateTime_yyyy_MM_ddTHH_mm_ssZ(Timestamp);
            set => Timestamp = NameAsTypeConverter.DateTimeToString_yyyy_MM_ddTHH_mm_ssZ(value);
        }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public Customer Customer { get; set; } = new Customer();

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        [Required(ErrorMessage = "Order Details are required")]
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<MedicationDetail> UsedMedicines
            => OrderDetails == null
                    ? new List<MedicationDetail>()
                    : OrderDetails.Where(od => od != null)
                        .SelectMany(od => od.IntakeDetails)
                            .Where(id => id != null)
                                .SelectMany(id => id.MedicationDetails)
                                    .Where(md => md != null && md.MedicineCode != null)
                                            .GroupBy(md => md.MedicineCode)
                                                .Select(g => g.First());

        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<string> UsedMedicineIds
            => UsedMedicines.Select(md => md.MedicineCode).Distinct();

        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<(string Id, string Name)> UsedMedicinesIdsAndName
            => UsedMedicines.Select(md => (md.MedicineCode, $"NoName {md.MedicineCode}"));
    }
}
