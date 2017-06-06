using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetCheersRequest : RequestBuilder
    {
        public GetCheersRequest(ulong? channelId) 
            : base("GET", "bits/actions")
        {
            if (channelId != null)
                _endpointParams.Add("channel_id", channelId);
        }
    }
}
