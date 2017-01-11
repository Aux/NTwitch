using NTwitch.Rest;

namespace NTwitch.Pubsub
{
    public class TwitchPubsubConfig : TwitchRestConfig
    {
        public string PubsubUrl { get; set; }
    }
}
