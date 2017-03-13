namespace NTwitch.Rest.Requests
{
    public class GetCurrentUserRequest : RestRequest
    {
        public GetCurrentUserRequest() : base("GET", "user") { }
    }
}
