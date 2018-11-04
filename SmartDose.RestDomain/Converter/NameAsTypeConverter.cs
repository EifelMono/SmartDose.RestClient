using System;
using System.Globalization;
using ModelsV2 = SmartDose.RestDomain.Models.V2;
using SmartDose.RestDomain;

namespace SmartDose.RestDomain.Converter
{
    public static class NameAsTypeConverter
    {
        #region Enum
        public const string FixNullAsString = "FixNull";
        public const string FixEmptyAsString = "FixEmpty";
        public const int FixNullAsValue = -1;
        public const int FixEmptyAsValue = -2;
        public static T StringToEnum<T>(string value) where T : Enum
        {
            if (value is null)
                return (T)Enum.ToObject(typeof(T), FixNullAsValue);
            if (value == "")
                return (T)Enum.ToObject(typeof(T), FixEmptyAsValue);
            try
            {
                var enumValue = (T)Enum.Parse(typeof(T), value, true);
                return enumValue;
            }
            catch { }
            return (T)Enum.ToObject(typeof(T), FixEmptyAsValue);
        }

        public static string EnumToString<T>(T value) where T : Enum
        {
            switch (Convert.ToInt16(value))
            {
                case FixEmptyAsValue:
                    return "";
                case FixNullAsValue:
                    return null;
                default:
                    return value.ToString();
            }
        }

        public static ModelsV2.CultureName StringToCultureName(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!(value.ToLower() == FixNullAsString.ToLower() || value.ToLower() == FixEmptyAsString.ToLower()))
                    if (value.StartsWith("cn_"))
                        value = value.Replace("-", "_");
                    else
                        value = $"cn_{value.Replace("-", "_")}";
            }
            return StringToEnum<ModelsV2.CultureName>(value);
        }

        public static string CultureNameToString(ModelsV2.CultureName value)
        {
            var result = EnumToString(value);
            if (result != null)
            {
                if (result.StartsWith("cn_"))
                    result = result.Replace("cn_", "");
                result = result.Replace("_", "-");
            }
            return result;
        }
        #endregion

        #region DateTime
        public static DateTime MinValue { get; private set; } = DateTime.Now.AddYears(-150);
        public static DateTime MaxValue { get; private set; } = DateTime.Now.AddYears(10);

        public static string DateTime_yyyy_MM_dd { get; private set; } = "yyyy-MM-dd";
        public static string DateTime_yyyy_MM_ddTHH_mm_ssZ { get; private set; } = "yyyy-MM-ddTHH:mm:ssZ";
        public static string DateTime_yyyy_MM_ddTHH_mm_ss { get; private set; } = "yyyy-MM-ddTHH:mm:ss";

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

        public static DateTime StringToDateTime_yyyy_MM_ddTHH_mm_ss(string value)
     => StringToDateTime(value, DateTime_yyyy_MM_ddTHH_mm_ss);
        public static string DateTimeToString_yyyy_MM_ddTHH_mm_ss(DateTime value)
             => value.ToString(DateTime_yyyy_MM_ddTHH_mm_ss, CultureInfo.InvariantCulture);
        #endregion
    }
}
