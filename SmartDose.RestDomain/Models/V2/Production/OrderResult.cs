﻿using SmartDose.RestDomain.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using System;

#if RestDomainDev
using SmartDose.Core;
using SmartDose.Core;
using System.Drawing.Design;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// Order result.
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
    /// <seealso cref="SmartDose.Production.RESTv2.Model" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class OrderResult
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
        /// Gets or sets the machine code.
        /// </summary>
        /// <value>
        /// The machine code.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Identifier of the machine which executes the Order is required.")]
        public string MachineCode { get; set; }

        /// <summary>
        /// Gets or sets the state of the dispense.
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
        /// Gets or sets the pouches.
        /// </summary>
        /// <value>
        /// The pouches.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "List of pouches is required.")]
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<Pouch> Pouches { get; set; } = new List<Pouch>();
    }
}
