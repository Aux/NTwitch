using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class UserCollection
    {
        [JsonProperty("_total")]
        public uint Total { get; set; }
        [JsonProperty("users")]
        public IEnumerable<User> Users { get; set; }
    }
}
