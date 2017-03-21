// Unabashedly copied from https://github.com/RogueException/Discord.Net async event impl

using System.Collections.Generic;
using System.Collections.Immutable;

namespace NTwitch
{
    public class AsyncEvent<T> where T : class
    {
        private readonly object _subLock = new object();
        internal ImmutableArray<T> _subscriptions;

        public IReadOnlyList<T> Subscriptions => _subscriptions;

        public AsyncEvent()
        {
            _subscriptions = ImmutableArray.Create<T>();
        }

        public void Add(T subscriber)
        {
            lock (_subLock)
                _subscriptions = _subscriptions.Add(subscriber);
        }
        public void Remove(T subscriber)
        {
            lock (_subLock)
                _subscriptions = _subscriptions.Remove(subscriber);
        }
    }
}
