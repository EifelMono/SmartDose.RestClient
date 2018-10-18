using System;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ExternalOrderSourceInfo
    {
        public ExternalOrderSourceInfo()
        {
        }

        /// <summary>When the order is receved. NOTE: witout milliseconds</summary>
        public DateTime Timestamp {get; set; }

        /// <summary>The ip adress from where the order is sended.</summary>
        public string IpAdress { get; set; }

        /// <summary>The original information recived by the REST service</summary>
        public string JsonString { get; set; }
        
        public long Id { get; set; }

        public bool Disabled { get; set; }
    }
}
