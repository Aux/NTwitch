using Newtonsoft.Json;
using System;
using System.Reflection;

namespace NTwitch.Rest
{
    internal static class EnumExtensions
    {
        public static string GetJsonValue(this Enum value)
        {
            var type = value.GetType().GetTypeInfo();
            var memberInfos = type.GetMember(value.ToString());
            var jsonProperty = memberInfos[0].GetCustomAttribute<JsonPropertyAttribute>();
            return jsonProperty.PropertyName;
        }
    }
}
