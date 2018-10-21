using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models
#else
namespace SmartDose.RestDomain.Models
#endif
{
    public static class ModelsGlobals
    {
        public const string ModelsName = "Models";
        public static Assembly ModelsAssembly => typeof(ModelsGlobals).Assembly;

        public static string ModelsNamespace => ModelsAssembly.GetName().Name;

        private static List<ModelsItem> _ModelsItems = null;
        public static List<ModelsItem> ModelsItems => _ModelsItems
                        ?? (_ModelsItems = (from type in ModelsAssembly.GetTypes()
                                            where type.IsClass && type.FullName.Contains($".{ModelsName}.V")
                                            let splitModelsFullPath = type.FullName.SplitModelsFullPath()
                                            select new ModelsItem
                                            {
                                                Type = type,
                                                FullName = type.FullName,
                                                Group = splitModelsFullPath.Ok ? splitModelsFullPath.Group : "",
                                                Version = splitModelsFullPath.Ok ? splitModelsFullPath.Version : "",
                                                Name = splitModelsFullPath.Ok ? splitModelsFullPath.Name : ""
                                            }).ToList());
        public static ModelsItem FindModelsItem(string fullName)
           => ModelsItems.Where(mi => mi.FullName == fullName).FirstOrDefault();
        public static ModelsItem FindModelsItem(Type type)
           => ModelsItems.Where(mi => mi.Type== type).FirstOrDefault();
    }
}
