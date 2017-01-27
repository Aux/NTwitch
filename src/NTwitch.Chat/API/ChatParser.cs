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
        public static T Parse<T>(TwitchMessage msg, BaseRestClient client)
        {
            var obj = CreateInstance<T>(new[] { client });
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
                            value = msg.Parameters.First().Substring(1);
                            break;
                        case PropertyType.Complex:
                            var method = typeof(ChatParser).GetRuntimeMethod("Parse", new[] { typeof(TwitchMessage), typeof(BaseRestClient) });
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

                    if (p.PropertyType == typeof(bool))
                        value = result == "1" ? true : false;
                    else
                        value = Convert.ChangeType(result, p.PropertyType);
                }

                if (value != null)
                    p.SetValue(obj, value);
            }

            return obj;
        }
        
        public static IEnumerable<PropertyInfo> GetProperties<T>(object obj) where T : Attribute
        {
            var type = obj.GetType().GetTypeInfo();
            return type.GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(T)));
        }

        public static T CreateInstance<T>(params object[] parameters)
        {
            var constructor = typeof(T).GetTypeInfo().DeclaredConstructors.First();
            var obj = constructor.Invoke(parameters);
            return (T)obj;
        }
    }
}
