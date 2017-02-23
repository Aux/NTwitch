using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
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
                var jProperty = p.GetCustomAttribute<JsonPropertyAttribute>();
                var jAlias = p.GetCustomAttribute<JsonPropertyAliasAttribute>();

                var names = new List<string>();
                names.Add(jProperty.PropertyName);
                if (jAlias != null)
                    names.AddRange(jAlias.Aliases);

                JToken token = null;
                string path = null;
                foreach (var name in names)
                {
                    path = _sub != null ? _sub + "." + name : name;
                    token = obj.SelectToken(path);

                    if (token != null)
                        break;
                }

                object propertyObject;
                if (CanConvert(p.PropertyType))
                {
                    propertyObject = ReflectionHelper.CreateInstance(p.PropertyType, _client);
                    
                    string json = JsonConvert.SerializeObject(token);
                    JsonConvert.PopulateObject(json, propertyObject);
                }
                else if (token.Type == JTokenType.Array && CanConvert(p.PropertyType.GetTypeInfo().GenericTypeArguments.First()))
                {
                    var genericType = p.PropertyType.GetTypeInfo().GenericTypeArguments.First();
                    var array = token as JArray;
                    var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));

                    foreach (var item in array)
                    {
                        var itemObject = ReflectionHelper.CreateInstance(genericType, _client);

                        string json = JsonConvert.SerializeObject(token);
                        JsonConvert.PopulateObject(item.ToString(), itemObject);
                        list.GetType().GetTypeInfo().GetMethod("Add").Invoke(list, new[] { itemObject });
                    }

                    propertyObject = list;
                }
                else
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
