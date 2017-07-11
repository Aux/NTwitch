using NTwitch.Pubsub.Queue;
using System.Collections.Generic;

namespace NTwitch.Pubsub.API
{
    public class VideoPlaybackRequest : PubsubRequestBuilder
    {
        public VideoPlaybackRequest(IEnumerable<ulong> channelIds, string authToken = null, bool listen = true) 
            : base(listen ? "LISTEN" : "UNLISTEN", authToken)
        {
            foreach (var channelId in channelIds)
                Topics.Add($"video-playback.{channelId}");
        }
    }
}
