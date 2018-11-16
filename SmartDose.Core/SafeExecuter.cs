using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.Core
{
    public static class SafeExecuter
    {
        public static void Catcher(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch { };
        }
    }
}
