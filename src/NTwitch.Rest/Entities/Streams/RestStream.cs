using Newtonsoft.Json;
using System;

namespace NTwitch.Rest
{
    public class RestStream : IEntity, IStream
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("")]
        public ulong Id { get; internal set; }
        [JsonProperty("")]
        public double AverageFps { get; internal set; }
        [JsonProperty("")]
        public RestChannel Channel { get; internal set; }
        [JsonProperty("")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("")]
        public int Delay { get; internal set; }
        [JsonProperty("")]
        public string Game { get; internal set; }
        [JsonProperty("")]
        public bool IsPlaylist { get; internal set; }
        [JsonProperty("")]
        public TwitchImage Preview { get; internal set; }
        [JsonProperty("")]
        public int VideoHeight { get; internal set; }
        [JsonProperty("")]
        public int Viewers { get; internal set; }

        internal RestStream(TwitchRestClient client)
        {
            Client = client;
        }

        internal static RestStream Create(BaseTwitchClient client, string json)
        {
            var stream = new RestStream(client as TwitchRestClient);
            JsonConvert.PopulateObject(json, stream);
            return stream;
        }

        ITwitchClient IEntity.Client
            => Client;
        IChannel IStream.Channel
            => Channel;
    }
}
