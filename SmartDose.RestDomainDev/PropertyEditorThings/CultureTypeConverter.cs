using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestDomainDev.PropertyEditorThings
{
    public class CultureTypeConverter : TypeConverter
    {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true; // display drop
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; // drop-down vs combo
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            return new StandardValuesCollection(cinfo.Select(c => c.Name).Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }
    }
}
