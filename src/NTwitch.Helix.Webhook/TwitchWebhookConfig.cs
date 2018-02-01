using System;
using NTwitch.Helix.Rest;

namespace NTwitch.Helix.Webhook
{
    public class TwitchWebhookConfig : TwitchRestConfig
    {
        public const int MaxSubscriptionCount = 1000;

        /// <summary> The url twitch uses to POST webhook events to </summary>
        public string CallbackUrl { get; set; } = "http://localhost:8081/ntwitch/webhook/client/";
        /// <summary> The default amount of time a subscription will stay alive </summary>
        public TimeSpan DefaultSubscriptionExpiry { get; set; } = TimeSpan.FromMilliseconds(864000);
    }
}
