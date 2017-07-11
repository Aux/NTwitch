using NTwitch.Pubsub.Queue;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    public class SubscriptionsRequest : PubsubRequestBuilder
    {
        public SubscriptionsRequest(IEnumerable<ulong> channelIds, string authtoken, bool listen = true) 
            : base(listen ? "LISTEN" : "UNLISTEN", authtoken)
        {
            foreach (var channelId in channelIds)
                Topics.Add($"channel-subscribe-events-v1.{channelId}");
        }
    }
}
