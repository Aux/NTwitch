using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class FollowCollection
    {
        [JsonProperty("follows")]
        public IEnumerable<Follow> Follows { get; set; }
    }
}
