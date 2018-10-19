using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartDose.RestDomainDev
{
    public static class RestDomainExtensions
    {
        public static JsonSerializerSettings DevObjJsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new SerializableExpandableContractResolver(),
            Formatting = Formatting.Indented
        };

        public static string ToJsonFromDevObject(this object thisValue)
           => JsonConvert.SerializeObject(thisValue, settings: DevObjJsonSerializerSettings);
        public static T ToDevObjectFromJson<T>(this string thisValue) where T : class
            => JsonConvert.DeserializeObject<T>(thisValue, settings: DevObjJsonSerializerSettings);

        public static string ToJsonFromObject(this object thisValue)
            => JsonConvert.SerializeObject(thisValue, Formatting.Indented);
        public static T ToObjectFromJson<T>(this string thisValue) where T : class
            => JsonConvert.DeserializeObject<T>(thisValue);

        public static To CloneToObjectForomDevObject<To, Tdo>(this Tdo thisValue) where Tdo : class
                                                                                  where To : class
         => thisValue.ToJsonFromDevObject().ToObjectFromJson<To>();
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
