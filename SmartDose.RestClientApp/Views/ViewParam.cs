using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Views
{
    public class ViewParam
    {
        public string Name { get; set; }
        public bool IsViewObjectJson { get; set; } = false;
        public object Value { get; set; }

        public object View { get; set; }
    }
}
