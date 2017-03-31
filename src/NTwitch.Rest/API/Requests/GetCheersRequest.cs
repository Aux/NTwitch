namespace NTwitch.Rest
{
    internal class GetCheersRequest : RestRequest
    {
        public GetCheersRequest(string token, ulong? channelId) 
            : base("GET", "bits/actions", token)
        {
            Parameters.Add("channel_id", channelId);
        }
    }
}