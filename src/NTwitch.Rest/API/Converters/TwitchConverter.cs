using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace NTwitch.Rest
{
    internal class TwitchConverter : JsonConverter
    {
        private BaseRestClient _client;
        private string _sub;

        public TwitchConverter(BaseRestClient client, string sub = null)
        {
            _client = client;
            _sub = sub;
        }

        public override bool CanConvert(Type objectType)
        {
            var interfaces = objectType.GetTypeInfo().GetInterfaces().Where(x => x.IsConstructedGenericType);

            if (interfaces.Any(x => x.GetGenericTypeDefinition() == typeof(IEntity<>)))
                return true;
            else
                return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var objectTypeInfo = objectType.GetTypeInfo();
            var properties = objectTypeInfo.GetProperties().Where(x => x.CanRead && x.CanWrite);

            var targetObj = ReflectionHelper.CreateInstance(objectType, _client);
            var obj = JObject.Load(reader);

            foreach (var p in properties)
            {
                var jProperty = p.GetCustomAttribute<TwitchJsonPropertyAttribute>();
                string path = string.Join(".", jProperty.Aliases);

                if (_sub != null)
                    path = _sub + "." + path;

                var token = obj.SelectToken(path);

                object propertyObject;
                if (CanConvert(p.PropertyType))
                {
                    propertyObject = ReflectionHelper.CreateInstance(p.PropertyType, _client);
                } else
                {
                    propertyObject = token.ToObject(p.PropertyType, serializer);
                }

                p.SetValue(targetObj, propertyObject, null);
            }

            return targetObj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
