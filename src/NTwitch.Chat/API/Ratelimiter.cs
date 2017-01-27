using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal class Ratelimiter
    {
        private readonly int _limitTotal;
        private readonly TimeSpan _limitSeconds;

        private DateTime _previousReset;
        private int _sendsRemaining;

        public bool IsLimited
            => _sendsRemaining < 1;
        public DateTime NextResetAt
            => _previousReset.AddSeconds(_limitSeconds.TotalSeconds);
        public double NextResetIn
            => (NextResetAt - _previousReset).TotalSeconds;

        public Ratelimiter(bool modOnly)
        {
            if (modOnly)
            {
                _limitTotal = 100;
                _limitSeconds = TimeSpan.FromSeconds(30);
            } else
            {
                _limitTotal = 20;
                _limitSeconds = TimeSpan.FromSeconds(30);
            }
        }

        public async Task TrySendAsync(Task task)
        {
            if (!IsLimited)
            {
                await task;
                _sendsRemaining--;

                if (DateTime.UtcNow > NextResetAt)
                {
                    _sendsRemaining = _limitTotal;
                    _previousReset = DateTime.UtcNow;
                }
            } else
            {
                throw new RatelimitedException(NextResetIn);
            }
        }
    }
}
