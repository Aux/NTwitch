namespace NTwitch.Rest
{
    internal class GetUsersRequest : RestRequest
    {
        public GetUsersRequest(string[] usernames)
            : base("GET", "users")
        {
            Parameters.Add("login", string.Join(",", usernames));
        }
    }
}