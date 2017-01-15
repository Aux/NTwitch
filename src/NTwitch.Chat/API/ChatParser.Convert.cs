using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NTwitch.Chat
{
    internal partial class ChatParser
    {
        public static void PopulateObject(string msg, object obj, BaseRestClient client)
        {
            var split = msg.Split(new[] { ' ' }, 2);
            string data = split[0];
            string content = split[1];

            var properties = GetProperties<ChatPropertyAttribute>(obj);

            foreach (var p in properties)
            {
                var attr = p.GetCustomAttribute<ChatPropertyAttribute>();

                object value;
                if (attr.Name == null)
                {
                    value = Activator.CreateInstance(p.GetType(), new[] { client });
                    PopulateObject(msg, value, client);
                } else
                {
                    string name = attr.Name + "=";
                    value = GetValueBetween(data, name, ";");
                }

                if (value != null)
                    p.SetValue(obj, value);
            }

            var betweens = GetProperties<ChatValueBetweenAttribute>(obj);

            foreach (var b in betweens)
            {
                var attr = b.GetCustomAttribute<ChatValueBetweenAttribute>();

                string value = GetValueBetween(content, attr.FromValue, attr.ToValue);

                if (value != null)
                    b.SetValue(obj, value);
            }
        }

        public static string GetValueBetween(string data, string start, string end)
        {
            int valueStart = data.IndexOf(start) + start.Count();
            if (valueStart < 0)
                return null;

            int valueEnd = data.Substring(valueStart).IndexOf(";");
            return data.Substring(valueStart, valueEnd - valueStart);
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>(object obj) where T : Attribute
        {
            var type = obj.GetType().GetTypeInfo();
            return type.GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(T)));
        }
    }
}
