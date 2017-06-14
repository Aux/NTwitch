using Model = NTwitch.Pubsub.API.Emote;

namespace NTwitch.Pubsub
{
    public class PubsubEmote : PubsubEntity<ulong>
    {
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }

        internal PubsubEmote(TwitchPubsubClient client, ulong id)
            : base(client, id) { }

        internal static PubsubEmote Create(TwitchPubsubClient client, Model model)
        {
            var entity = new PubsubEmote(client, id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            StartIndex = model.StartIndex;
            EndIndex = model.EndIndex;
        }
    }
}
