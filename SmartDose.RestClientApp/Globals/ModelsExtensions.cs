using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartDose.RestClientApp.Globals
{
    public static class ModelsExtensions
    {
        public static T FillEmtpyModels<T>(this T thisValue) where T : class
        {
            if (thisValue is null)
                return thisValue;
            if (thisValue.GetType().IsArray)
                foreach (var item in thisValue as IEnumerable<object>)
                    FillEmtpyModels(item);

            foreach (var property in thisValue.GetType().GetProperties())
            {
                if (property.PropertyType.IsClass)
                {
                    if (property.PropertyType.FullName.StartsWith(ModelsGlobals.ModelsNamespace))
                    {
                        var value = property.GetValue(thisValue);
                        if (value is null)
                        {
                            try
                            {
                                if (property.PropertyType.IsArray)
                                {
                                    value = Activator.CreateInstance(property.PropertyType, 0);
                                    property.SetValue(thisValue, value);
                                }
                                else
                                {
                                    value = Activator.CreateInstance(property.PropertyType);
                                    property.SetValue(thisValue, value);
                                    FillEmtpyModels(value);
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.LogException();
                            }
                        }
                    }
                }
            }
            return thisValue;
        }

        public static T RemoveEmtpyModels<T>(this T thisValue) where T : class
        {
            // todo remove
            return thisValue;
        }

        public static (bool Ok, string Group, string Version, string Name) SplitModelsFullPath(this string thisValue)
        {
            if (thisValue is null)
                return (false, "", "", "");
            try
            {
                var splitName = thisValue.Split('.');
                var name = splitName.Last();
                var version = splitName.Skip(3).Take(1).FirstOrDefault();
                var group = splitName.Skip(4).Take(1).FirstOrDefault();
                return (true, group, version, name);
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
            return (false, "", "", "");
        }

        public static (bool Ok, string Group, string Version, string Name) SplitModelsFullPath(this Type thisValue)
            => thisValue is null ? (false, "", "", "") : thisValue.FullName.SplitModelsFullPath();
        public static (bool Ok, string Group, string Version, string Name) SplitModelsFullPath(this object thisValue)
            => thisValue is null ? (false, "", "", "") : thisValue.GetType().SplitModelsFullPath();

        public static string ModelsDirectory(this object thisValue)
        {
            if (thisValue.SplitModelsFullPath() is var result && result.Ok)
                return Path.Combine(ModelsGlobals.ModelsName, result.Version, result.Group, result.Name);
            return "";
        }

        public static string ToJson(this object thisObject) => JsonConvert.SerializeObject(thisObject,
                settings: new JsonSerializerSettings()
                {
                    ContractResolver = new SerializableExpandableContractResolver(),
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });
    }

    public class SerializableExpandableContractResolver : DefaultContractResolver
    {
        public SerializableExpandableContractResolver()
        {
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (TypeDescriptor.GetAttributes(objectType).Contains(new TypeConverterAttribute(typeof(ExpandableObjectConverter))))
            {
                return CreateObjectContract(objectType);
            }
            return base.CreateContract(objectType);
        }
    }
}
