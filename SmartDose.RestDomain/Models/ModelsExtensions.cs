﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models
#else
namespace SmartDose.RestDomain.Models
#endif
{
    internal static class ModelsExtensions
    {
        internal static (bool Ok, string Group, string Version, string Name, bool IsDev) SplitModelsFullPath(this string thisValue)
        {
            if (thisValue is null)
                return (false, "", "", "", false);
            try
            {
                var splitName = thisValue.Split('.');
                return (true,
                        splitName.Skip(4).Take(1).FirstOrDefault(),
                        splitName.Skip(3).Take(1).FirstOrDefault(),
                        splitName.Last(),
                        splitName.Skip(1).Take(1).FirstOrDefault().EndsWith("Dev"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return (false, "", "", "", false);
        }

        public static (bool Ok, string Group, string Version, string Name, bool IsDev) SplitModelsFullPath(this Type thisValue)
            => thisValue is null ? (false, "", "", "", false) : thisValue.FullName.SplitModelsFullPath();
        public static (bool Ok, string Group, string Version, string Name, bool IsDev) SplitModelsFullPath(this object thisValue)
            => thisValue is null ? (false, "", "", "", false) : thisValue.GetType().SplitModelsFullPath();
    }
}
