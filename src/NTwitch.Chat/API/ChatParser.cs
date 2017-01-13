using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal static class ChatParser
    {
        public static Task<Type> GetType(string msg)
        {
            string content = msg.Split(' ')[1];

            int startIndex = content.IndexOf(":tmi.twitch.tv ");
            if (startIndex < 0)
            {
                startIndex = content.IndexOf(":jtv ");
                if (startIndex < 0)
                    throw new InvalidOperationException();
            }
            
            int endIndex = content.Substring(startIndex).IndexOf(' ');

            string type = content.Substring(startIndex, endIndex);

            switch (type)
            {
                case "JOIN":
                    return Task.FromResult(typeof(ChatMessage));
                case "PART":
                    return Task.FromResult(typeof(ChatMessage));
                case "MODE":
                    return Task.FromResult(typeof(ChatMessage));
                case "NOTICE":
                    return Task.FromResult(typeof(ChatMessage));
                case "HOSTTARGET":
                    return Task.FromResult(typeof(ChatMessage));
                case "CLEARCHAT":
                    return Task.FromResult(typeof(ChatMessage));
                case "USERSTATE":
                    return Task.FromResult(typeof(ChatMessage));
                case "RECONNECT":
                    return Task.FromResult(typeof(ChatMessage));
                case "ROOMSTATE":
                    return Task.FromResult(typeof(ChatMessage));
                case "USERNOTICE":
                    return Task.FromResult(typeof(ChatMessage));
                case "PRIVMSG":
                    return Task.FromResult(typeof(ChatMessage));
                case "GLOBALUSERSTATE":
                    return Task.FromResult(typeof(ChatMessage));
                default:
                    throw new NotSupportedException("The message type `" + type + "` is not supported at this time.");
            }
        }

        public static void PopulateObject(string msg, object obj)
        {
            var split = msg.Split(' ');
            string data = split[0];
            string content = split[1];

            var properties = GetProperties<ChatPropertyAttribute>(obj);

            foreach (var p in properties)
            {
                var attr = p.GetCustomAttribute<ChatPropertyAttribute>();

                string name = attr.Name + "=";
                string value = GetValueBetween(data, name, ";");

                if (value != null)
                    p.SetValue(obj, value);
            }

            var betweens = GetProperties<ChatValueBetweenAttribute>(obj);

            foreach (var b in betweens)
            {
                var attr = b.GetCustomAttribute<ChatValueBetweenAttribute>();

                string value = GetValueBetween(content, attr.FromValue, attr.ToValue);

                if (value != null)
                    p.SetValue(obj, value);
            }
        }

        public static string GetValueBetween(string data, string start, string end)
        {
            int valueStart = data.IndexOf(start);
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
