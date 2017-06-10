using NTwitch.Pubsub.Queue;

namespace NTwitch.Pubsub.API
{
    public class ListenWhispersRequest : PubsubRequestBuilder
    {
        public ListenWhispersRequest(ulong[] userIds, string authtoken) 
            : base("LISTEN", authtoken)
        {
            foreach (var userId in userIds)
                Topics.Add($"whispers.{userId}");
        }
    }
}
