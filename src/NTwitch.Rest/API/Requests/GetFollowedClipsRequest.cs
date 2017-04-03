namespace NTwitch.Rest.API
{
    internal class GetFollowedClipsRequest : RestRequest
    {
        public GetFollowedClipsRequest(string token, bool istrending, uint limit)
            : base("GET", "clips/followed", token)
        {
            Parameters.Add("trending", istrending);
            Parameters.Add("limit", limit);
        }
    }
}
