using System.Linq;
using System.Threading.Tasks;
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

        public async Task UpdateAsync(RequestOptions options = null)
        {
            var models = await Twitch.ApiClient.GetGamesAsync(new[] { Id }, null, options: options).ConfigureAwait(false);
            Update(models.SingleOrDefault());
        }
    }
}
