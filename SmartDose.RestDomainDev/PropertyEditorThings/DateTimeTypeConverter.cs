using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartDose.RestDomain.Converter;

namespace SmartDose.RestDomainDev.PropertyEditorThings
{
    public class DateTimeTypeConverter : TypeConverter
    {
        public DateTimeTypeConverter(string customFormat)
        {
            CustomFormat = customFormat;
        }
        private string CustomFormat { get; set; }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                try
                {
                    return DateTime.ParseExact(s, CustomFormat, CultureInfo.InvariantCulture);
                }
                catch
                {
                    return value;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                try
                {
                    return ((DateTime)value).ToString(CustomFormat);
                }
                catch
                {
                    return DateTimeGlobals.MinValue;
                }


            return base.ConvertTo(context, culture, value, destinationType);
        }

    }

    public class Date_yyyy_MM_dd_TypeConverter : DateTimeTypeConverter
    {
        public Date_yyyy_MM_dd_TypeConverter() : base(DateTimeGlobals.DateTime_yyyy_MM_dd) { }
    }

    public class Date_yyyy_MM_ddTHH_mm_ssZ_TypeConverter : DateTimeTypeConverter
    {
        public Date_yyyy_MM_ddTHH_mm_ssZ_TypeConverter() : base(DateTimeGlobals.DateTime_yyyy_MM_ddTHH_mm_ssZ) { }
    }
}
