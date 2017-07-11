using Newtonsoft.Json;
using System;

namespace NTwitch.Pubsub.API
{
    internal class CommerceEvent : BaseEvent
    {
        [JsonProperty("item_image_url")]
        public string ItemImageUrl { get; set; }
        [JsonProperty("item_description")]
        public string ItemDescription { get; set; }
        [JsonProperty("supports_channel")]
        public bool SupportsChannel { get; set; }
        [JsonProperty("purchase_message")]
        public Message Message { get; set; }
    }
}
