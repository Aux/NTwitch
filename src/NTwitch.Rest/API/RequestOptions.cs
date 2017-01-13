using System.Collections.Generic;

namespace NTwitch.Rest
{
    public class RequestOptions
    {
        public int ApiVersion { get; set; } = 5;
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public object Payload { get; set; }
    }
}
