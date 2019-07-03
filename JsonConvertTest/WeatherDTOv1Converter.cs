using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConvertTest
{
    public class WeatherDTOConverterv1:JsonConverter
    {
        private readonly Dictionary<string, string> _propertyMappings = new Dictionary<string, string>
        {
            {"dated", "date"},
            {"temperature", "temperatureF"},
            {"summary", "description"},
            {"feelslike", "description"}
        };
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsClass;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object instance = Activator.CreateInstance(objectType);
            var props = objectType.GetTypeInfo().DeclaredProperties.ToList();

            JObject jo = JObject.Load(reader);
            foreach (JProperty jp in jo.Properties())
            {
                if (!_propertyMappings.TryGetValue(jp.Name, out var name))
                    name = jp.Name;

                PropertyInfo prop = props.FirstOrDefault(pi =>
                    pi.CanWrite && pi.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == name);

                prop?.SetValue(instance, jp.Value.ToObject(prop.PropertyType, serializer));

                if (jp.Name == "temperature")
                {
                    PropertyInfo relatedProp = props.FirstOrDefault(pi =>
                        pi.CanWrite && pi.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == "temperatureC");
                    relatedProp?.SetValue(instance, Math.Round(((Convert.ToDecimal(jp.Value) - 32) * 5 / 9),2) );
                }
            }

            return instance;
        }
    }
}
