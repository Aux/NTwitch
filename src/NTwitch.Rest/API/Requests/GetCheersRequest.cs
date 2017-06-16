using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetCheersRequest : RestRequestBuilder
    {
        public GetCheersRequest(ulong? channelId) 
            : base("GET", "bits/actions")
        {
            if (channelId != null)
                Parameters.Add("channel_id", channelId);
        }
    }
}
