using NTwitch.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal partial class ChatParser
    {
        public static Task<T> ParseAsync<T>(TwitchMessage msg, BaseRestClient client)
        {
            var obj = Activator.CreateInstance(typeof(T), client);
            var properties = GetProperties<ChatPropertyAttribute>(obj);

            foreach (var p in properties)
            {
                var attr = p.GetCustomAttribute<ChatPropertyAttribute>();

                object value;
                if (attr.Name == null)
                {
                    switch (attr.Type)
                    {
                        case PropertyType.Content:
                            value = msg.Parameters.Last();
                            break;
                        case PropertyType.ChannelName:
                            value = msg.Parameters.First();
                            break;
                        case PropertyType.Complex:
                            var method = typeof(ChatParser).GetRuntimeMethod("ParseAsync", new[] { typeof(TwitchMessage), typeof(BaseRestClient) });
                            var generic = method.MakeGenericMethod(p.PropertyType);
                            value = generic.Invoke(null, new object[] { msg, client });
                            break;
                        default:
                            throw new FormatException("Property type is not valid for the given context.");
                    }
                }
                else
                {
                    string result;
                    if (!msg.Tags.TryGetValue(attr.Name, out result))
                        throw new ArgumentOutOfRangeException("The tag " + attr.Name + " was not found.");

                    value = Convert.ChangeType(result, p.PropertyType);
                }

                if (value != null)
                    p.SetValue(obj, value);
            }

            return Task.FromResult((T)obj);
        }
        
        public static IEnumerable<PropertyInfo> GetProperties<T>(object obj) where T : Attribute
        {
            var type = obj.GetType().GetTypeInfo();
            return type.GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(T)));
        }
    }
}
