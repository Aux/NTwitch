using NTwitch.Pubsub.Queue;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    public class ListenVideoPlaybackRequest : PubsubRequestBuilder
    {
        public ListenVideoPlaybackRequest(IEnumerable<ulong> channelIds, string authToken = null) 
            : base("LISTEN", authToken)
        {
            foreach (var channelId in channelIds)
                Topics.Add($"video-playback.{channelId}");
        }
    }
}
