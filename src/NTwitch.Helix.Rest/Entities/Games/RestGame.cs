using Model = NTwitch.Helix.API.Game;

namespace NTwitch.Helix.Rest
{
    public class RestGame : RestEntity<ulong>
    {
        public string Name { get; private set; }
        public string BoxArtUrl { get; private set; }
        
        internal RestGame(BaseTwitchClient twitch, ulong id)
            : base(twitch, id) { }
        internal static RestGame Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestGame(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
            Name = model.Name;
            BoxArtUrl = model.BoxArtUrl;
        }
    }
}
