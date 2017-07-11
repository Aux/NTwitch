using System;
using Model = NTwitch.Pubsub.API.BaseEvent;

namespace NTwitch.Pubsub
{
    public class PubsubEvent
    {
        /// <summary> An instance of the client that created this event </summary>
        public TwitchPubsubClient Client { get; }
        /// <summary> The date and time that this event occurred </summary>
        public DateTime Timestamp { get; private set; }
        /// <summary> The channel this event occurred in </summary>
        public PubsubSimpleChannel Channel { get; private set; }
        /// <summary> The user that initiated this event </summary>
        public PubsubSimpleUser User { get; private set; }

        internal PubsubEvent(TwitchPubsubClient client)
        {
            Client = client;
        }

        internal static PubsubEvent Create(TwitchPubsubClient client, Model model)
        {
            var entity = new PubsubEvent(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Timestamp = model.Timestamp;
            Channel = PubsubSimpleChannel.Create(Client, model);
            User = PubsubSimpleUser.Create(Client, model);
        }
    }
}
