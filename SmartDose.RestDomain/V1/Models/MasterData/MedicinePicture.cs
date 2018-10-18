using System.ComponentModel;

namespace SmartDose.RestDomain.V1.Models.MasterData
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MedicinePicture
    {
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
