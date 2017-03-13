using System.Collections.Generic;

namespace NTwitch.Rest
{
    internal class GetUsersRequest : RestRequest
    {
        public GetUsersRequest(string[] usernames) : base("GET", "users?{0}", new Dictionary<string, object>()
        {
            { "login", string.Join(",", usernames) }
        }) { }
    }
}