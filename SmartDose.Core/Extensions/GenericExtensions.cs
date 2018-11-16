using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.Core.Extensions
{
    public static class GenericExtensions
    {
        public static T Pipe<T>(this T thisValue, Action<T> action)
        {
            action?.Invoke(thisValue);
            return thisValue;
        }

        public static T Pipe<T>(this T thisValue, Func<T, T> func)
        => func.Invoke(thisValue);
    }
}
