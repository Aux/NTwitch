using System.Collections.Generic;
using Newtonsoft.Json;

namespace NTwitch.Rest.API
{
    internal class UserData
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }

        [JsonProperty("users")]
        public Optional<IReadOnlyCollection<User>> Users { get; set; }
        [JsonProperty("blocks")]
        public Optional<IReadOnlyCollection<User>> Blocks { get; set; }
    }
}
