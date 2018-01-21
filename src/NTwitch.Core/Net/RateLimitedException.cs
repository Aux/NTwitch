using System;

namespace NTwitch
{
    public class RateLimitedException : TimeoutException
    {
        public RateLimitedException()
            : base("You are being rate limited.")
        {
        }
    }
}
