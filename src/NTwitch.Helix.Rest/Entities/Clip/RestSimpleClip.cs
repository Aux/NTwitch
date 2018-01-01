using Model = NTwitch.Helix.API.Clip;

namespace NTwitch.Helix.Rest
{
    public class RestSimpleClip : RestEntity<string>
    {
        public string Url { get; private set; }
        public string EditUrl { get; private set; }

        internal RestSimpleClip(BaseTwitchClient twitch, string id)
            : base(twitch, id) { }
        internal static RestSimpleClip Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestSimpleClip(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
            if (model.Url.IsSpecified)
                Url = model.Url.Value;
            else
                Url = GetUrl();
            if (model.EditUrl.IsSpecified)
                EditUrl = model.EditUrl.Value;
            else
                EditUrl = GetEditUrl();
        }

        protected virtual string GetUrl()
            => TwitchConfig.ClipsUrl + Id;
        protected virtual string GetEditUrl()
            => GetUrl() + "/edit";
    }
}
