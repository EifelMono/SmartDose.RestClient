using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.WcfClient
{
    public class WcfErrorObject
    {
        public WcfErrorObject(string message, Exception ex= null)
        {
            Message = message;
            Exception = ex;
        }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
