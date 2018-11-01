using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.RestDomain.Converter
{
    public static class DateTimeGlobals
    {
        public static DateTime MinValue { get; private set; } = DateTime.Now.AddYears(-150);
        public static DateTime MaxValue { get; private set; } = DateTime.Now.AddYears(10);

        public static string DateTime_yyyy_MM_dd { get; private set; } = "yyyy-MM-dd";
        public static string DateTime_yyyy_MM_ddTHH_mm_ssZ { get; private set; } = "yyyy-MM-ddTHH:mm:ssZ";

        public static string DateTime_yyyy_MM_ddTHH_mm_ss { get; private set; } = "yyyy-MM-ddTHH:mm:ss";
    }
}
