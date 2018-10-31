using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestGenerator.Generator.V0
{
    public class BirthdayGenerator
    {
        private static Random s_random = new Random();

        public static DateTime RandomBirthday()
        {
            var year = s_random.Next(1920, DateTime.Now.Year - 10);
            var month = s_random.Next(1, 12);
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }
    }
}
