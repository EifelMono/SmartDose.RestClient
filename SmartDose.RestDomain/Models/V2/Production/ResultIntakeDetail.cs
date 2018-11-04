using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using Newtonsoft.Json;
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
    /// Represents the details of one intake time.
    /// </summary>
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif

    public class ResultIntakeDetail
    {
        /// <summary>
        /// Gets or sets the intake date time.
        /// </summary>
        /// <value>
        /// The intake date time.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ss", "IntakeDateTime requires this format yyyy-MM-ddTHH:mm:ss")]
        public string IntakeDateTime { get; set; }
#if RestDomainDev
        [TypeConverter(typeof(Date_yyyy_MM_dd_TypeConverter))]
        [Editor(typeof(DateTime_yyyy_MM_ddTHH_mm_ss_Editor), typeof(UITypeEditor))]
#endif
        [JsonIgnore]
        public DateTime IntakeDateTimeAsType
        {
            get => NameAsTypeConverter.StringToDateTime_yyyy_MM_ddTHH_mm_ss(IntakeDateTime);
            set => IntakeDateTime = NameAsTypeConverter.DateTimeToString_yyyy_MM_ddTHH_mm_ss(value);
        }

        /// <summary>
        /// Gets or sets the medication details.
        /// </summary>
        /// <value>
        /// The medication details.
        /// </value>
        [Required(ErrorMessage = "ResultMedicationDetails is required")]
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<ResultMedicationDetail> ResultMedicationDetails { get; set; } = new List<ResultMedicationDetail>();

        /// <summary>
        /// Gets or sets the pouches.
        /// </summary>
        /// <value>
        /// The pouches.
        /// </value>
        [Required(ErrorMessage = "Pouches is required")]
        public List<Pouch> Pouches { get; set; }
    }
}
