using System.Collections.Generic;
using System.Collections.Specialized;

namespace NTwitch.Helix.Webhook
{
    internal static class NameValueCollectionExtensions
    {
        public static Dictionary<string, string> ToDictionary(this NameValueCollection values)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var key in values.AllKeys)
                dictionary.Add(key, values[key]);
            return dictionary;
        }
    }
}
