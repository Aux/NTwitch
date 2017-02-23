using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NTwitch.Rest
{
    public class TwitchEntityConverter : JsonConverter
    {
        private TwitchCollectionConverter _converter;
        private BaseRestClient _client;
        private string _path;

        public TwitchEntityConverter(BaseRestClient client = null, string path = null)
        {
            _client = client;
            _path = path;
        }

        public override bool CanConvert(Type objectType)
        {
            var interfaces = objectType.GetTypeInfo().GetInterfaces().Where(x => x.IsConstructedGenericType);
            return interfaces.Any(x => x.GetGenericTypeDefinition() == typeof(IEntity<>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            _converter = new TwitchCollectionConverter(_client, converter: this);
            var objectTypeInfo = objectType.GetTypeInfo();
            var properties = objectTypeInfo.GetProperties().Where(x => x.CanRead && x.CanWrite);

            var targetObj = ReflectionHelper.CreateInstance(objectType, _client);
            var obj = JObject.Load(reader);

            foreach (var p in properties)
            {
                var names = GetJsonNames(p);
                var token = GetJToken(obj, names);

                object propertyObject;
                if (!CanConvert(p.PropertyType))
                {
                    if (token.Type == JTokenType.Array)
                    {
                        var genericType = p.PropertyType.GetTypeInfo().GenericTypeArguments.First();
                        propertyObject = _converter.CreateCollection(genericType, token as JArray);
                    } else
                    {
                        propertyObject = token.ToObject(p.PropertyType, serializer);
                    }
                } else
                {
                    propertyObject = CreateObject(p.PropertyType, token);
                }

                p.SetValue(targetObj, propertyObject, null);
            }

            return targetObj;
        }

        public object CreateObject(Type type, JToken token)
        {
            var newObject = ReflectionHelper.CreateInstance(type, _client);
            JsonConvert.PopulateObject(token.ToString(), newObject);
            return newObject;
        }

        public IEnumerable<string> GetJsonNames(PropertyInfo property)
        {
            var jProperty = property.GetCustomAttribute<JsonPropertyAttribute>();
            var jAlias = property.GetCustomAttribute<JsonPropertyAliasAttribute>();

            var names = new List<string>();
            names.Add(jProperty.PropertyName);
            if (jAlias != null)
                names.AddRange(jAlias.Aliases);

            return names;
        }
        
        public JToken GetJToken(JObject jobject, IEnumerable<string> names)
        {
            foreach (var name in names)
            {
                string path = _path == null ? name : $"{_path}.{name}";
                
                var token = jobject.SelectToken(path);

                if (token != null)
                    return token;
            }

            throw new KeyNotFoundException($"No matches found for the specified json property aliases: `{string.Join(", ", names)}`");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
