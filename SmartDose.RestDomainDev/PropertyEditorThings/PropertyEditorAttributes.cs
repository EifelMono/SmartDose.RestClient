using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestDomainDev.PropertyEditorThings
{
    // [AttributeUsage(AttributeTargets.Property)]
    public class CategoryAsStringAttribute : CategoryAttribute
    {
        public CategoryAsStringAttribute(): base ("AsString")
        {
        }
    }

    // [AttributeUsage(AttributeTargets.Property)]
    public class CategoryAsTypeAttribute : CategoryAttribute
    {
        public CategoryAsTypeAttribute() : base("AsType")
        {
        }
    }
}
