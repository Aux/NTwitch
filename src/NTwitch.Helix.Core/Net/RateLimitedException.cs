using System;

namespace NTwitch.Helix
{
    public class RateLimitedException : TimeoutException
    {
        public RateLimitedException()
            : base("You are being rate limited.")
        {
        }
    }
}
