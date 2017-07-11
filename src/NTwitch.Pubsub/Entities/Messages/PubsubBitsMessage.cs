using Model = NTwitch.Pubsub.API.BitsMessageEvent;

namespace NTwitch.Pubsub
{
    public class PubsubBitsMessage : PubsubMessage
    {
        /// <summary>  </summary>
        public uint BitsUsed { get; private set; }
        /// <summary>  </summary>
        public uint TotalBitsUsed { get; private set; }
        /// <summary>  </summary>
        public string Context { get; private set; }
        /// <summary>  </summary>
        public BitsBadge Badge { get; private set; }

        internal PubsubBitsMessage(TwitchPubsubClient client, string id)
            : base(client, id) { }
        
        internal new static PubsubBitsMessage Create(TwitchPubsubClient client, Model model)
        {
            var entity = new PubsubBitsMessage(client, model.MessageId);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            BitsUsed = model.Data.BitsUsed;
            TotalBitsUsed = model.Data.TotalBitsUsed;
            Context = model.Data.Context;
            Badge = model.Data.Badge;
        }
    }
}
