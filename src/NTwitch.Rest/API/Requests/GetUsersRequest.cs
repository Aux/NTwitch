using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetUsersRequest : RestRequestBuilder
    {
        public GetUsersRequest(string[] usernames) 
            : base("GET", "users")
        {
            Parameters.Add("login", string.Join(",", usernames));
        }
    }
}
