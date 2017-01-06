using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestChannelSubscription : RestSubscription, IChannelSubscription
    {
        [JsonProperty("")]
        public IChannel Channel { get; internal set; }

        public RestChannelSubscription(TwitchRestClient client) : base(client) { }
        
        IChannel IChannelSubscription.Channel
            => Channel;
    }
}
