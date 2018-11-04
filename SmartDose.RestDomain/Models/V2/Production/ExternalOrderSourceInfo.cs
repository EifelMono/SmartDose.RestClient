using System;
using System.ComponentModel;

using Newtonsoft.Json;
using SmartDose.RestDomain.Converter;
using SmartDose.RestDomain.Validation;

#if RestDomainDev
using System.Drawing.Design;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V2.Production
#else
namespace SmartDose.RestDomain.Models.V2.Production
#endif
{
    /// <summary>
    /// External Order Source Info
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class ExternalOrderSourceInfo
    {
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        [DateTimeValidation("yyyy-MM-ddTHH:mm:ssZ", "Timestamp is required with yyyy-MM-ddTHH:mm:ssZ format.")]
        public string Timestamp { get; set; }
#if RestDomainDev
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
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IpAdress { get; set; }

        /// <summary>
        /// Gets or sets the json string.
        /// </summary>
        /// <value>
        /// The json string.
        /// </value>
        public string JsonString { get; set; }
    }
}
