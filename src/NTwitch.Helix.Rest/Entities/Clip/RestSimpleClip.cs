using Model = NTwitch.Helix.API.Clip;

namespace NTwitch.Helix.Rest
{
    public class RestSimpleClip : RestEntity<string>
    {
        internal RestSimpleClip(BaseTwitchClient twitch, Model model)
            : base(twitch, model.Id) { }
        internal static RestSimpleClip Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestSimpleClip(twitch, model);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
        }

        public virtual string GetUrl()
            => TwitchConfig.ClipsUrl + Id;
    }
}
