using NTwitch.Pubsub.Queue;

namespace NTwitch.Pubsub.API
{
    public class ListenBitsRequest : PubsubRequestBuilder
    {
        public ListenBitsRequest(ulong[] channelIds, string authtoken) 
            : base("LISTEN", authtoken)
        {
            foreach (var channelId in channelIds)
                Topics.Add($"channel-bits-events-v1.{channelId}");
        }
    }
}
