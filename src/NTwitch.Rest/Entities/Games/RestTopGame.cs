using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestTopGame : RestEntity
    {
        [JsonProperty("channels")]
        public int ChannelTotal { get; internal set; }
        [JsonProperty("game")]
        public RestGame Game { get; internal set; }
        [JsonProperty("viewers")]
        public int ViewerTotal { get; internal set; }

        public RestTopGame(BaseRestClient client) : base(client) { }

        public static RestTopGame Create(BaseRestClient client, string json)
        {
            var game = new RestTopGame(client);
            JsonConvert.PopulateObject(json, game);
            return game;
        }
    }
}
