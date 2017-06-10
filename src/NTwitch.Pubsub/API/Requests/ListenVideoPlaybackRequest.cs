using NTwitch.Pubsub.Queue;

namespace NTwitch.Pubsub.API
{
    public class ListenVideoPlaybackRequest : PubsubRequestBuilder
    {
        public ListenVideoPlaybackRequest(ulong[] channelIds, string authToken = null) 
            : base("LISTEN", authToken)
        {
            foreach (var channelId in channelIds)
                Topics.Add($"video-playback.{channelId}");
        }
    }
}
