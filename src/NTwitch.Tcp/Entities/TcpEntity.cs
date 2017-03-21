using NTwitch.Rest;
using System;

namespace NTwitch.Tcp
{
    public class TcpEntity<T> : IEntity<T>
    {
        public BaseRestClient Client { get; }
        public T Id { get; }

        public TcpEntity(BaseRestClient client, T id)
        {
            Client = client;
            Id = id;
        }

        ITwitchClient IEntity<T>.Client
            => throw new NotImplementedException();
    }
}
