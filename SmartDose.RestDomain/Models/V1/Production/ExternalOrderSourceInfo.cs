using System;
using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
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
