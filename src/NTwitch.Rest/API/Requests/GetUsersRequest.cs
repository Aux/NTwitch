using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetUsersRequest : RequestBuilder
    {
        public GetUsersRequest(string[] usernames) 
            : base("GET", "users")
        {
            _endpointParams.Add("login", string.Join(",", usernames));
        }
    }
}
