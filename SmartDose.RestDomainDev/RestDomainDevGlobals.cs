using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Serialization;
using SmartDose.Core.Extensions;

namespace SmartDose.RestDomainDev
{
    public static class RestDomainDevGlobals
    {

        public static string ToJsonFromObjectDev(object objectDev)
            => objectDev.ToJsonFromExpandableObject();
        public static object ToObjectDevFromJson(string value, Type type)
            => value.ToExpandableObjectFromJson(type);

        public static object ToObjectFromObjectDev(object objectDev)
        {
            if (objectDev is null)
                return null;
            var json = ToJsonFromObjectDev(objectDev);
            if ((from i in objectDev.GetType().GetInterfaces()
                 where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                 select i?.GetGenericArguments()[0]).FirstOrDefault() is var genericType && genericType != null)
            {
                if (Models.ModelsGlobals.FindModelsItem(genericType.FullName.Replace(".RestDomainDev.", ".RestDomain.")) is var item && item != null)
                {
                    object dummyArray = Array.CreateInstance(item.Type, 0);
                    return json.ToExpandableObjectFromJson(dummyArray.GetType());
                }
            }
            else
            {
                if (RestDomain.Models.ModelsGlobals.FindModelsItem(objectDev.GetType().FullName.Replace(".RestDomainDev.", ".RestDomain.")) is var item && item != null)
                    return json.ToExpandableObjectFromJson(item.Type);
            }
            return null;
        }
        public static object ToObjectDevFromObject(object objectValue)
        {
            if (objectValue is null)
                return null;
            var json = ToJsonFromObjectDev(objectValue);
            if ((from i in objectValue.GetType().GetInterfaces()
                 where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                 select i?.GetGenericArguments()[0]).FirstOrDefault() is var genericType && genericType != null)
            {
                if (Models.ModelsGlobals.FindModelsItem(genericType.FullName.Replace(".RestDomain.", ".RestDomainDev.")) is var item && item != null)
                {
                    object dummyArray = Array.CreateInstance(item.Type, 0);
                    return json.ToExpandableObjectFromJson(dummyArray.GetType());
                }
            }
            else
            {
                if (Models.ModelsGlobals.FindModelsItem(objectValue.GetType().FullName.Replace(".RestDomain.", ".RestDomainDev.")) is var item && item != null)
                    return json.ToExpandableObjectFromJson(item.Type);
            }
            throw new Exception("no dev object found");
        }
    }
}
