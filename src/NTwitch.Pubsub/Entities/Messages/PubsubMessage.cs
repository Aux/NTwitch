using System;
using BitsModel = NTwitch.Pubsub.API.BitsMessageEvent;
using CommerceModel = NTwitch.Pubsub.API.CommerceEvent;

namespace NTwitch.Pubsub
{
    public class PubsubMessage : PubsubEntity<string>, IMessage
    {
        /// <summary>  </summary>
        public DateTime Timestamp { get; private set; }
        /// <summary>  </summary>
        public string Content { get; private set; }
        /// <summary>  </summary>
        public PubsubSimpleChannel Channel { get; private set; }
        /// <summary>  </summary>
        public PubsubSimpleUser User { get; private set; }

        internal PubsubMessage(TwitchPubsubClient client, string id)
            : base(client, id) { }

        public bool Equals(IMessage other)
            => Id == other.Id;

        internal static PubsubMessage Create(TwitchPubsubClient client, BitsModel model)
        {
            var entity = new PubsubMessage(client, model.MessageId);
            entity.Update(model);
            return entity;
        }

        internal static PubsubMessage Create(TwitchPubsubClient client, CommerceModel model)
        {
            var entity = new PubsubMessage(client, null);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(BitsModel model)
        {
            Timestamp = model.Data.Timestamp;
            Content = model.Data.Content;

            Channel = PubsubSimpleChannel.Create(Client, model);
            User = PubsubSimpleUser.Create(Client, model);
        }

        internal virtual void Update(CommerceModel model)
        {
            Timestamp = model.Timestamp;
            Content = model.Message.Content;

            Channel = PubsubSimpleChannel.Create(Client, model);
            User = PubsubSimpleUser.Create(Client, model);
        }
    }
}
