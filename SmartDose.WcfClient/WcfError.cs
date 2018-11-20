using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.WcfClient
{
    public class WcfError
    {
        public WcfError(string message, Exception exception= null)
        {
            Message = message;
            Exception = exception;
        }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
