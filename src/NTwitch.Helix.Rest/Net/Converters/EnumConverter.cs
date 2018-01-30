using System;
using Newtonsoft.Json;

namespace NTwitch.Helix.Net.Converters
{
    internal class EnumConverter : JsonConverter
    {
        public static readonly EnumConverter Instance = new EnumConverter();

        public override bool CanConvert(Type objectType) => true;
        public override bool CanRead => true;
        public override bool CanWrite => true;
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var stringValue = existingValue?.ToString();
            if (string.IsNullOrWhiteSpace(stringValue))
                return Activator.CreateInstance(objectType);
            else
                return Enum.Parse(objectType, stringValue);
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteValue(value);
        }
    }
}
