using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Globals
{
    public class ModelsItem
    {
        public Type Type { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Group { get; set; }
        public object Value { get; set; }

        public override string ToString()
            => FullName;
    }
}
