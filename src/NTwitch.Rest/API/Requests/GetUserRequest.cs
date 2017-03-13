namespace NTwitch.Rest.Requests
{
    public class GetUserRequest : RestRequest
    {
        public GetUserRequest(ulong id) : base("GET", $"users/{id}") { }
    }
}
