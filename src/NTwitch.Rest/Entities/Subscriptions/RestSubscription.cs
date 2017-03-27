using System;
using Model = NTwitch.Rest.API.Subscription;

namespace NTwitch.Rest
{
    public class RestSubscription
    {
        /// <summary> The instance of the client that created this entity </summary>
        public BaseRestClient Client { get; }
        /// <summary> The date and time this subscription was created </summary>
        public DateTime CreatedAt { get; private set; }

        internal RestSubscription(BaseRestClient client)
        {
            Client = client;
        }

        internal static RestSubscription Create(BaseRestClient client, Model model)
        {
            var entity = new RestSubscription(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            CreatedAt = model.CreatedAt;
        }
    }
}
