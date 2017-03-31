namespace NTwitch.Rest
{
    internal class GetUsersRequest : RestRequest
    {
        public GetUsersRequest(string token, string[] usernames)
            : base("GET", "users", token)
        {
            Parameters.Add("login", string.Join(",", usernames));
        }
    }
}