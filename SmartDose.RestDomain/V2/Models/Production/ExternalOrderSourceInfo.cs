﻿using System.ComponentModel;
using SmartDose.RestDomain.Validation;

namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// External Order Source Info
    /// </summary>
    /// <seealso cref="SmartDose.Production.RESTV2.Models.BaseData" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
