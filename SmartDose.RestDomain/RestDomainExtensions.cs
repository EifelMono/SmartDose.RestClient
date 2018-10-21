using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.RestDomain
{
    public static class RestDomainExtensions
    {
        public static string ToRfId(this int thisValue) => $"{thisValue:00000000}";
        public static string ToRotorId(this int thisValue) => $"{thisValue:0000000}-{thisValue:00000}-{thisValue:00000}";
    }
}
