using Model = NTwitch.Pubsub.API.BadgeEntitlement;

namespace NTwitch.Pubsub
{
    public class PubsubBitsBadge
    {
        public string Before { get; private set; }
        public string After { get; private set; }

        internal static PubsubBitsBadge Create(Model model)
        {
            var entity = new PubsubBitsBadge();
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Before = model.OldVersion;
            After = model.NewVersion;
        }
    }
}
