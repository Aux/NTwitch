using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NTwitch.Rest
{
    public class TwitchCollectionConverter : JsonConverter
    {
        private TwitchEntityConverter _converter;
        private BaseRestClient _client;
        private string _path;

        public TwitchCollectionConverter(BaseRestClient client = null, string path = null)
        {
            _client = client;
            _path = path;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetType() == typeof(IEnumerable<>) 
                && _converter.CanConvert(objectType.GetTypeInfo().GenericTypeArguments.First());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            _converter = new TwitchEntityConverter(_client);
            var objectTypeInfo = objectType.GetTypeInfo();
            var obj = JObject.Load(reader);
            var token = obj.SelectToken(_path);

            if (token.Type != JTokenType.Array)
                throw new ArgumentException("Targetted path is not an array.");

            var genericType = objectType.GetTypeInfo().GenericTypeArguments.First();
            return CreateCollection(genericType, token as JArray);
        }

        public object CreateCollection(Type genericType, JArray token)
        {
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
            
            foreach (var item in token)
            {
                var itemObject = _converter.CreateObject(genericType, item);
                list.GetType().GetTypeInfo().GetMethod("Add").Invoke(list, new[] { itemObject });
            }

            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
