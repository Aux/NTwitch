using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace NTwitch
{
    public class TwitchConverter : JsonConverter
    {
        private string _name;

        public TwitchConverter(string name)
        {
            _name = name;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<string>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var obj = JObject.Load(reader);
            
            var list = new List<string>();
            foreach (var item in obj[_name])
                list.Add(JsonConvert.SerializeObject(item));

            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
