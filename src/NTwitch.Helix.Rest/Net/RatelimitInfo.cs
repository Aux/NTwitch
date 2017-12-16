using System;
using System.Collections.Generic;

namespace NTwitch.Helix.Rest
{
    internal struct RateLimitInfo
    {
        public int? Limit { get; }
        public int? Remaining { get; }
        public DateTimeOffset? Reset { get; }
        public TimeSpan? Lag { get; }

        internal RateLimitInfo(Dictionary<string, string> headers)
        {
            Limit = headers.TryGetValue("RateLimit-Limit", out string temp) &&
                int.TryParse(temp, out var limit) ? limit : (int?)null;
            Remaining = headers.TryGetValue("RateLimit-Remaining", out temp) &&
                int.TryParse(temp, out var remaining) ? remaining : (int?)null;
            Reset = headers.TryGetValue("RateLimit-Reset", out temp) &&
                int.TryParse(temp, out var reset) ? DateTimeUtils.FromUnixSeconds(reset) : (DateTimeOffset?)null;
            Lag = headers.TryGetValue("Date", out temp) &&
                DateTimeOffset.TryParse(temp, out var date) ? DateTimeOffset.UtcNow - date : (TimeSpan?)null;
        }
    }
}
