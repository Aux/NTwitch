using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public class EmoticonImageCollection
    {
        [JsonProperty("regex")]
        public string Regex { get; }
        [JsonProperty("emoticons")]
        public IEnumerable<EmoticonImage> Emoticons { get; }
    }
}
