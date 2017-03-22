namespace NTwitch.Rest
{
    internal class GetCheersRequest : RestRequest
    {
        public GetCheersRequest(ulong? channelId) 
            : base("GET", "bits/actions")
        {
            Parameters.Add("channel_id", channelId);
        }
    }
}