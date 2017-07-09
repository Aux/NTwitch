using NTwitch.Pubsub.Queue;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    public class ListenWhispersRequest : PubsubRequestBuilder
    {
        public ListenWhispersRequest(IEnumerable<ulong> userIds, string authtoken) 
            : base("LISTEN", authtoken)
        {
            foreach (var userId in userIds)
                Topics.Add($"whispers.{userId}");
        }
    }
}
