namespace NTwitch.Rest
{
    internal class GetUsersRequest : OldRestRequest
    {
        public GetUsersRequest(string token, string[] usernames)
            : base("GET", "users", token)
        {
            Parameters.Add("login", string.Join(",", usernames));
        }
    }
}