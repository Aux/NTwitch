using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelFollow : RestFollow, IChannelFollow
    {
        [JsonProperty("")]
        public RestChannel Channel { get; internal set; }

        internal RestChannelFollow(TwitchRestClient client) : base(client) { }

        IChannel IChannelFollow.Channel
            => Channel;
    }
}
