using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartDose.RestClient.Crud;

namespace SmartDose.RestClient
{
    public static class UrlConfig
    {
        public static string UrlV1 = "http://127.0.0.1:6040/SmartDose";
        // http://localhost:6040/SmartDose/Customers
        public static string UrlV2 = "http://127.0.0.1:56040/SmartDose/V2.0";
        // http://localhost:6040/SmartDose/V2.0/MasterData/Customers
        public static void ClearUrls()
        {
            foreach (var type in typeof(UrlConfig).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Core))))
            {
                if (type.GetMethod("ClearInstance", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance) is MethodInfo method)
                {
                    try
                    {
                        method.Invoke(null, null);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}

