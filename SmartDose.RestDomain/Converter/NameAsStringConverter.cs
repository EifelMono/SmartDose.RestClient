using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SmartDose.RestDomain.Converter
{
    public static class NameAsStringConvert
    {
        #region Enum
        public const int Null = -1;
        public const int Empty = -2;
        public static T StringToEnum<T>(string value) where T : Enum
        {
            if (value is null)
                return (T)Enum.ToObject(typeof(T), Null);
            if (value == "")
                return (T)Enum.ToObject(typeof(T), Empty);
            try
            {
                var enumValue = (T)Enum.Parse(typeof(T), value, true);
                return enumValue;
            }
            catch { }
            return (T)Enum.ToObject(typeof(T), Empty);
        }

        public static string EnumToString<T>(T value) where T : Enum
        {
            switch (Convert.ToInt16(value))
            {
                case Empty:
                    return "";
                case Null:
                    return null;
                default:
                    return value.ToString();
            }
        }
        #endregion

        #region DateTime
        public static DateTime MinValue { get; private set; } = DateTime.Now.AddYears(-150);
        public static DateTime MaxValue { get; private set; } = DateTime.Now.AddYears(10);

        public static string DateTime_yyyy_MM_dd { get; private set; } = "yyyy-MM-dd";
        public static string DateTime_yyyy_MM_ddTHH_mm_ssZ { get; private set; } = "yyyy-MM-ddTHH:mm:ssZ";

        public static DateTime StringToDateTime(string value, string format)
        {
            if (string.IsNullOrEmpty(value))
                return MinValue;
            try
            {
                if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var datetime))
                    return datetime;
            }
            catch { }
            return MinValue;
        }

        public static string DateTimeToString(DateTime value, string format)
            => value.ToString(format, CultureInfo.InvariantCulture);

        public static DateTime StringToDateTime_yyyy_MM_dd(string value)
            => StringToDateTime(value, DateTime_yyyy_MM_dd);
        public static string DateTimeToString_yyyy_MM_dd(DateTime value)
            => value.ToString(DateTime_yyyy_MM_dd, CultureInfo.InvariantCulture);

        public static DateTime StringToDateTime_yyyy_MM_ddTHH_mm_ssZ(string value)
            => StringToDateTime(value, DateTime_yyyy_MM_ddTHH_mm_ssZ);
        public static string DateTimeToString_yyyy_MM_ddTHH_mm_ssZ(DateTime value)
         => value.ToString(DateTime_yyyy_MM_ddTHH_mm_ssZ, CultureInfo.InvariantCulture);
        #endregion
    }
}
