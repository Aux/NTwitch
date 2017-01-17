using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestUserSummary : UserBase
    {
        [JsonProperty("logo")]
        public string LogoUrl { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; private set; }
        
        internal RestUserSummary(BaseRestClient client) : base(client) { }

        internal static RestUserSummary Create(BaseRestClient client, string json)
        {
            var user = new RestUserSummary(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
    }
}
