using System;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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
