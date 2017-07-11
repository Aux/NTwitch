using System.Collections.Generic;
using System.Linq;
using Model = NTwitch.Pubsub.API.CommerceEvent;

namespace NTwitch.Pubsub
{
    public class PubsubCommerceEvent : PubsubEvent
    {
        /// <summary>  </summary>
        public string ItemImageUrl { get; private set; }
        /// <summary>  </summary>
        public string ItemDescription { get; private set; }
        /// <summary>  </summary>
        public bool SupportsChannel { get; private set; }
        /// <summary>  </summary>
        public string Message { get; private set; }
        /// <summary>  </summary>
        public IReadOnlyCollection<PubsubEmote> Emotes { get; private set; }

        internal PubsubCommerceEvent(TwitchPubsubClient client)
            : base(client) { }

        internal static PubsubCommerceEvent Create(TwitchPubsubClient client, Model model)
        {
            var entity = new PubsubCommerceEvent(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            base.Update(model);
            ItemImageUrl = model.ItemImageUrl;
            ItemDescription = model.ItemDescription;
            SupportsChannel = model.SupportsChannel;

            Message = model.Message.Content;
            Emotes = model.Message.Emotes.Select(x => PubsubEmote.Create(Client, x)).ToArray();
        }
    }
}
