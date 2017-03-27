using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class GameCollection
    {
        [JsonProperty("games")]
        public IEnumerable<Game> Games { get; set; }
    }
}
