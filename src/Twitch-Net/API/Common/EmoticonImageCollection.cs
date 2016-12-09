using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public class EmoticonCollection
    {
        [JsonProperty("regex")]
        public string Regex { get; }
        [JsonProperty("emoticons")]
        public IEnumerable<EmoticonImage> Emoticons { get; }
    }
}
