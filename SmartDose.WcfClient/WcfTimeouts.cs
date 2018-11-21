using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.WcfClient
{
    public class WcfTimeouts
    {
        public TimeSpan Open { get; set; } = TimeSpan.FromSeconds(5);
        public TimeSpan Receive { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan Send { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan Close { get; set; } = TimeSpan.FromSeconds(5);

        public static readonly WcfTimeouts Defaults = new WcfTimeouts();
    }
}
