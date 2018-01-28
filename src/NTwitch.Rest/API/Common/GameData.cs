using Newtonsoft.Json;
using System.Collections.Generic;

namespace NTwitch.Rest.API
{
    internal class GameData
    {
        [JsonProperty("games")]
        public IReadOnlyCollection<Game> Games { get; set; }
    }
}
