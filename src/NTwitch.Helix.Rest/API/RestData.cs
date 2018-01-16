using System.Collections.Generic;
using Newtonsoft.Json;

namespace NTwitch.Helix.API
{
    internal class RestData<T>
    {
        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }
        [JsonProperty("pagination")]
        public Optional<Pagination> Pagination { get; set; }
        [JsonProperty("total")]
        public Optional<int> Total { get; set; }
    }
}
