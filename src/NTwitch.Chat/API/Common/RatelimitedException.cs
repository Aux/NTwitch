using System;

namespace NTwitch.Chat
{
    public class RatelimitedException : TimeoutException
    {
        public RatelimitedException(double timeUntilNext) 
            : base($"You're about to hit the ratelimit! ({timeUntilNext}s)") { }
    }
}
