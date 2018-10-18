
using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.Production
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class InternalOrder
    {
       public string Identifier { get; set; }
    }
}
