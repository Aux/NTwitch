using System;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.Video;

namespace NTwitch.Helix.Rest
{
    public class RestVideo : RestEntity<ulong>
    {
        public ulong UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime PublishedAt { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public int ViewCount { get; private set; }
        public string Language { get; private set; }
        public TimeSpan Duration { get; private set; }

        internal RestVideo(BaseTwitchClient twitch, ulong id)
            : base(twitch, id) { }
        internal static RestVideo Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestVideo(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
            UserId = model.UserId;
            Title = model.Title;
            Description = model.Description;
            CreatedAt = model.CreatedAt;
            PublishedAt = model.PublishedAt;
            ThumbnailUrl = model.ThumbnailUrl;
            ViewCount = model.ViewCount;
            Language = model.Language;
            Duration = model.Duration;
        }
        
        public async Task<RestUser> GetUserAsync(RequestOptions options = null)
            => (await ClientHelper.GetUsersAsync(Twitch, new[] { UserId }, options: options).ConfigureAwait(false)).SingleOrDefault();
    }
}
