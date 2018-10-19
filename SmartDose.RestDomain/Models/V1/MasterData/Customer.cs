using System;
using System.ComponentModel;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models.V1.MasterData
#else
namespace SmartDose.RestDomain.Models.V1.MasterData
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Fax { get; set; }
        public ContactAddress ContactAddress { get; set; }
    
        public ContactPerson ContactPerson { get; set; }

        public object ModelExtensions()
        {
            throw new NotImplementedException();
        }
    }

}
