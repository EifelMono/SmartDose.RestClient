using System;

namespace SmartDose.RestDomain.V1.Models
{
    public class SpecialHandling
    {
        public bool Narcotic { get; set; }

        public bool SeperatePouch { get; set; }

        public bool RobotHandling { get; set; }

        public bool NeedsCooling { get; set; }

        public bool Splitable { get; set; }

        public int MaxAmountPerPouch { get; set; }
    }
}
