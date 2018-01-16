using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class Pagination
    {
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
    }
}
