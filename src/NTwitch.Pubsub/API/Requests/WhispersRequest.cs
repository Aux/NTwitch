using NTwitch.Pubsub.Queue;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    public class WhispersRequest : PubsubRequestBuilder
    {
        public WhispersRequest(IEnumerable<ulong> userIds, string authtoken, bool listen = true) 
            : base(listen ? "LISTEN" : "UNLISTEN", authtoken)
        {
            foreach (var userId in userIds)
                Topics.Add($"whispers.{userId}");
        }
    }
}
