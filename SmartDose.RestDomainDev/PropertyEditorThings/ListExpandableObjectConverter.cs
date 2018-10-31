using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestDomainDev.PropertyEditorThings
{
    public class ListExpandableObjectConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Initializes a new instance of the System.ComponentModel.ExpandableObjectConverter class.
        /// </summary>
        public ListExpandableObjectConverter()
        {
        }

        /// <summary>
        /// Gets a collection of properties for the type of object
        /// specified by the value parameter.
        /// </summary>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var x = value.GetType().GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (x!= null)
            {
                var y = x.GetValue(value);
                if (y != null)
                    return TypeDescriptor.GetProperties(y, attributes);
            }
            return TypeDescriptor.GetProperties(value, attributes);
        }

        /// <summary>
        /// Gets a value indicating whether this object supports properties using the
        /// specified context.
        /// </summary>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
    }
}
