
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RestPill
    {
        public string MedicineId { get; set; }

        public string Quantity { get; set; }

        public string Charge { get; set; }
    }
}
