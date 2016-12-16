using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch
{
    public class Emoticon
    {
        [JsonProperty("id")]
        public uint Id { get; }
        [JsonProperty("emoticon_set")]
        public uint? SetId { get; }
        [JsonProperty("code")]
        public string Code { get; }
    }
}
