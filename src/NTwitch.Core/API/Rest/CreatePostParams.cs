using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch
{
    public class CreatePostParams
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("share")]
        public bool Share { get; set; }
    }
}
