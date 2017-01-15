﻿using Newtonsoft.Json;

namespace NTwitch.Rest
{
    public class RestPostPermissions
    {
        [JsonProperty("")]
        public bool? CanDelete { get; internal set; }
        [JsonProperty("")]
        public bool? CanModerate { get; internal set; }
        [JsonProperty("")]
        public bool? CanReply { get; internal set; }
    }
}