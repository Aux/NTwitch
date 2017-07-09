using NTwitch.Pubsub.Queue;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    public class ListenSubscriptionsRequest : PubsubRequestBuilder
    {
        public ListenSubscriptionsRequest(IEnumerable<ulong> channelIds, string authtoken) 
            : base("LISTEN", authtoken)
        {
            foreach (var channelId in channelIds)
                Topics.Add($"channel-subscribe-events-v1.{channelId}");
        }
    }
}
