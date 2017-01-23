using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace NTwitch.Rest
{
    public class JsonPathConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var targetObj = Activator.CreateInstance(objectType);

            var obj = JObject.Load(reader);
            var properties = objectType.GetTypeInfo().GetProperties().Where(x => x.CanRead && x.CanWrite);
            foreach (var p in properties)
            {
                var jproperty = p.GetCustomAttributes(true).OfType<JsonPropertyAttribute>().FirstOrDefault();

                if (jproperty == null)
                    continue;
                
                var token = obj.SelectToken(jproperty.PropertyName);

                if (token != null && token.Type != JTokenType.Null)
                {
                    object value = token.ToObject(p.PropertyType, serializer);
                    p.SetValue(targetObj, value, null);
                }
            }

            return targetObj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
