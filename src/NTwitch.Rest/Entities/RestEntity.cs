using System;

namespace NTwitch.Rest
{
    public class RestEntity<T> : IEntity<T>
    {
        public BaseRestClient Client { get; }
        public T Id { get; }

        public RestEntity(BaseRestClient client, T id)
        {
            Client = client;
            Id = id;
        }
        
        ITwitchClient IEntity<T>.Client 
            => throw new NotImplementedException();
    }
}
