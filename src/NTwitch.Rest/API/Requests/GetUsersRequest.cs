using System.Collections.Generic;

namespace NTwitch.Rest
{
    internal class GetUsersRequest : RestRequest
    {
        public GetUsersRequest(string[] usernames) : base("GET", "users", new Dictionary<string, object>()
        {
            { "login", string.Join(",", usernames) }
        }) { }
    }
}