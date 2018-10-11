using System;

namespace SmartDose.RestDomain.V1.Models
{
    public class Synonym
    {
        public string Identifier { get; set; }
        public decimal Price { get; set; }

        public int Content { get; set; }

        public string Description { get; set; }
    }
}
