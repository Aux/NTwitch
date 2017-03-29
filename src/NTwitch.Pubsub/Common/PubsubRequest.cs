using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace NTwitch.Pubsub
{
    public class PubsubRequest
    {
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("nonce", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Nonce { get; private set; }
        [JsonProperty("data", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PubsubRequestData Data { get; private set; }

        private Stopwatch _timer { get; }

        public PubsubRequest(string type)
        {
            _timer = new Stopwatch();
            Type = type;

            _timer.Start();
        }

        public PubsubRequest WithData(string token, params string[] topics)
        {
            Nonce = new Guid().ToString();
            Data = new PubsubRequestData(topics, token);
            return this;
        }

        public long GetTime()
        {
            _timer.Stop();
            return _timer.ElapsedMilliseconds;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
