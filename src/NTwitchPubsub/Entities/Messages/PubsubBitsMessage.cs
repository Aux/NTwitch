using System;
using Model = NTwitch.Pubsub.API.BitsMessage;

namespace NTwitch.Pubsub
{
    public class PubsubBitsMessage : PubsubEntity<string>
    {
        public PubsubSimpleChannel Channel { get; private set; }
        public PubsubSimpleUser User { get; private set; }
        public PubsubBitsBadge Badge { get; private set; }
        public DateTime Timestamp { get; private set; }
        public uint BitsUsed { get; private set; }
        public uint TotalBitsUsed { get; private set; }
        public string Context { get; private set; }
        public string Type { get; private set; }
        public string Version { get; private set; }

        internal PubsubBitsMessage(BasePubsubClient client, string id)
            : base(client, id) { }

        internal static PubsubBitsMessage Create(BasePubsubClient client, Model model)
        {
            var entity = new PubsubBitsMessage(client, model.MessageId);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Channel = new PubsubSimpleChannel(Client, model.ChannelId);
            Channel.Update(model);
            User = new PubsubSimpleUser(Client, model.UserId);
            User.Update(model);

            BitsUsed = model.BitsUsed;
            TotalBitsUsed = model.TotalBitsUsed;
            Context = model.Context;
            Type = model.MessageType;
            Version = model.Version;
        }
    }
}
