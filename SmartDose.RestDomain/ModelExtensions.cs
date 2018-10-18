using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestDomain
{
    public static class ModelExtensions
    {
        public static T CreateAllSubModels<T>(this T rootObject) where T : class
        {
            if (rootObject is null)
                return rootObject;
            foreach (var property in rootObject.GetType().GetProperties())
            {
                if (property.PropertyType.IsClass)
                {
                    if (property.PropertyType.FullName.Contains(typeof(ModelExtensions).Namespace))
                    {
                        var value = property.GetValue(rootObject);
                        if (value is null)
                        {
                            try
                            {
                                if (property.PropertyType.IsArray)
                                {
                                    value = Activator.CreateInstance(property.PropertyType, 0);
                                    property.SetValue(rootObject, value);
                                }
                                else
                                {
                                    value = Activator.CreateInstance(property.PropertyType);
                                    property.SetValue(rootObject, value);
                                    CreateAllSubModels(value);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            return rootObject;
        }

        private static List<ModelItem> _ModelItems = null;
        public static List<ModelItem> ModelItems => _ModelItems
                        ?? (_ModelItems = (from agt in typeof(ModelExtensions).Assembly.GetTypes()
                                           where agt.IsClass &&  agt.FullName.Contains(".Models.")
                                           let SplitName= agt.FullName.Split('.')
                                           select new ModelItem
                                           {
                                               FullName = agt.FullName,
                                               Name = agt.Name,
                                               Version = SplitName.Skip(2).Take(1).FirstOrDefault(),
                                               Group = SplitName.Skip(4).Take(1).FirstOrDefault()
                                           }).ToList());

    }


    public class ModelItem
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Group { get; set; }
        public object Value { get; set; }

        public override string ToString()
            => FullName;
    }
}
