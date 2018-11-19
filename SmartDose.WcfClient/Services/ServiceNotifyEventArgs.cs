using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.WcfClient.Services
{
    public class ServiceNotifyEventArgs : EventArgs
    {
        public ServiceNotifyEvent Value { get; set; }
    }
}
