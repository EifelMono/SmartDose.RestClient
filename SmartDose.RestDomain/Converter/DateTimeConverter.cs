using System;
using System.Globalization;
using Newtonsoft.Json;

namespace SmartDose.RestDomain.Converter
{
    public class DateTimeConverter : JsonConverter
    {
        public DateTimeConverter(string customFormat)
        {
            CustomFormat = customFormat;
        }

        public string CustomFormat { get; set; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteValue(DateTimeGlobals.MinValue.ToString(CustomFormat));
            }
            else
            {
                writer.WriteValue(((DateTime)value).ToString(CustomFormat));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = reader.Value;
            if (obj is string dateTimeAsString)
            {
                var dateTime = DateTimeGlobals.MinValue;
                if (!string.IsNullOrEmpty(dateTimeAsString))
                {
                    try
                    {
                        dateTime = DateTime.ParseExact(dateTimeAsString, CustomFormat, CultureInfo.InvariantCulture);
                    }
                    catch { }
                    return DateTimeGlobals.MinValue;
                }
                else
                    return DateTimeGlobals.MinValue;
            }
            else
                if (obj is DateTime)
                return (DateTime)obj;
            return DateTimeGlobals.MinValue;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }

    public class DateTime_yyyy_MM_dd_Converter : DateTimeConverter
    {
        public DateTime_yyyy_MM_dd_Converter() : base(DateTimeGlobals.DateTime_yyyy_MM_dd) { }
    }

    public class DateTime_yyyy_MM_ddTHH_mm_ssZ_Converter : DateTimeConverter
    {
        public DateTime_yyyy_MM_ddTHH_mm_ssZ_Converter() : base(DateTimeGlobals.DateTime_yyyy_MM_ddTHH_mm_ssZ) { }
    }
}
