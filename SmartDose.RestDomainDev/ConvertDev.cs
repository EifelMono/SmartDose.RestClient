﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartDose.RestDomainDev
{
    public static class ConvertDev
    {

        public static JsonSerializerSettings JsonSerializerSettingsDev = new JsonSerializerSettings()
        {
            ContractResolver = new SerializableExpandableContractResolver(),
            Formatting = Formatting.Indented
        };

        public static string ToJsonFromObjectDev(object objectDev)
           => JsonConvert.SerializeObject(objectDev, settings: JsonSerializerSettingsDev);
        public static T ToObjectDevFromJson<T>(string value) where T : class
            => JsonConvert.DeserializeObject<T>(value, settings: JsonSerializerSettingsDev);
        public static object ToObjectDevFromJson(string value, Type type) 
          => JsonConvert.DeserializeObject(value, type, JsonSerializerSettingsDev);

        public static string ToJsonFromObject(object objectValue)
            => JsonConvert.SerializeObject(objectValue, Formatting.Indented);
        public static T ToObjectFromJson<T>(string objectValue) where T : class
            => JsonConvert.DeserializeObject<T>(objectValue);
        public static object ToObjectFromJson(string value, Type type)
        => JsonConvert.DeserializeObject(value, type);

        public static object ToObjectFromObjectDev(object objectDev)
        {
            if (objectDev is null)
                return null;
            var json = ToJsonFromObjectDev(objectDev);
            if (RestDomain.Models.ModelsGlobals.FindModelsItem(objectDev.GetType().FullName.Replace(".RestDomainDev.", ".RestDomain.")) is var item && item != null)
                return JsonConvert.DeserializeObject(json, item.Type, JsonSerializerSettingsDev);
            return null;
        }
        public static object ToObjectDevFromObject(object objectValue)
        {
            if (objectValue is null)
                return null;
            var json = ToJsonFromObjectDev(objectValue);
            if (RestDomainDev.Models.ModelsGlobals.FindModelsItem(objectValue.GetType().FullName.Replace(".RestDomain.", ".RestDomainDev.")) is var item && item != null)
                return JsonConvert.DeserializeObject(json, item.Type, JsonSerializerSettingsDev);
            return null;
        }
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
