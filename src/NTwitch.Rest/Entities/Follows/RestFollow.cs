using System;
using Model = NTwitch.Rest.API.Follow;

namespace NTwitch.Rest
{
    public class RestFollow
    {
        public BaseRestClient Client { get; }
        public DateTime CreatedAt { get; private set; }
        public bool Notifications { get; private set; }

        internal RestFollow(BaseRestClient client)
        {
            Client = client;
        }

        internal static RestFollow Create(BaseRestClient client, Model model)
        {
            var entity = new RestFollow(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            CreatedAt = model.CreatedAt;
            Notifications = model.Notifications;
        }
    }
}
