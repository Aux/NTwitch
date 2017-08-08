using System;
using Model = NTwitch.Rest.API.Follow;

namespace NTwitch.Rest
{
    public class RestFollow
    {
        /// <summary> The instance of the client that created this entity </summary>
        public BaseTwitchClient Client { get; }
        /// <summary> Date and time when this follow was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> Information about this follow's notification settings </summary>
        public bool Notifications { get; private set; }

        internal RestFollow(BaseTwitchClient client)
        {
            Client = client;
        }

        internal static RestFollow Create(BaseTwitchClient client, Model model)
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
