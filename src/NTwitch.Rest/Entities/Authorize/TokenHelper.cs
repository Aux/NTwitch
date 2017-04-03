using System;
using System.Linq;

namespace NTwitch.Rest
{
    internal static class TokenHelper
    {
        public static bool TryGetToken(BaseRestClient client, ulong id, out RestTokenInfo info)
        {
            if (client.Tokens.TryGetValue(id, out info))
                return true;
            else
                return false;
        }

        public static string GetSingleToken(BaseRestClient client)
        {
            string clientid = client.RestClient.ClientId;
            
            if (client.Tokens.Values.Count() > 1 && string.IsNullOrWhiteSpace(clientid))
                throw new InvalidOperationException("A client id must be specified for multi-token management.");
            
            var token = client.Tokens.Values.FirstOrDefault();
            if (token != null)
                return token.Token;
            else
                return null;
        }
    }
}
