using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class FollowCollection<T> where T : Follow
    {
        [JsonProperty("follows")]
        public IEnumerable<T> Follows { get; set; }
    }
}
