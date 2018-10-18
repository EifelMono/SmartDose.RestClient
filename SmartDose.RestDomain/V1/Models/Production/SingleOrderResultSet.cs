using System.Collections.Generic;
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SingleOrderResultSet
    {
        public string ExternalID { get; set; }

        public string OrderId { get; set; }

        public string MachineNumber { get; set; }

        public string DispenseState { get; set; }

        public string CreateDate { get; set; }

        public string ProduceDate { get; set; }

        public RestPouch[] Pouches { get; set; }
    }
}
