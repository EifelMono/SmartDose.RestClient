using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SmartDose.WcfClient
{
    public class WcfMethode
    {
        public string Name { get; set; }

        public MethodInfo Method { get; set; }

        public object Input { get; set; }

        public object Output { get; set; }
    }
}
