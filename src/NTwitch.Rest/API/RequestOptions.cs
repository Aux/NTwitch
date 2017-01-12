using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RequestOptions
    {
        public int ApiVersion { get; set; } = 5;
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public object Payload { get; set; }
    }
}
