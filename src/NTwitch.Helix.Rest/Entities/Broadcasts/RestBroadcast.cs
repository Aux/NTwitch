using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.Broadcast;

namespace NTwitch.Helix.Rest
{
    public class RestBroadcast : RestEntity<ulong>
    {
        public ulong UserId { get; private set; }
        public ulong GameId { get; private set; }
        public IReadOnlyCollection<ulong> CommunityIds { get; private set; }
        public string Type { get; private set; }
        public string Title { get; private set; }
        public int ViewerCount { get; private set; }
        public DateTime StartedAt { get; private set; }
        public string Language { get; private set; }
        public string ThumbnailImageUrl { get; private set; }

        internal RestBroadcast(BaseTwitchClient twitch, ulong id)
            : base(twitch, id) { }
        internal static RestBroadcast Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestBroadcast(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
            if (model.UserId.IsSpecified)
                UserId = model.UserId.Value;
            if (model.GameId.IsSpecified)
                GameId = model.GameId.Value;
            if (model.CommunityIds.IsSpecified)
                CommunityIds = model.CommunityIds.Value.ToReadOnlyCollection();
            if (model.Type.IsSpecified)
                Type = model.Type.Value;
            if (model.Title.IsSpecified)
                Title = model.Title.Value;
            if (model.ViewerCount.IsSpecified)
                ViewerCount = model.ViewerCount.Value;
            if (model.StartedAt.IsSpecified)
                StartedAt = model.StartedAt.Value;
            if (model.Language.IsSpecified)
                Language = model.Language.Value;
            if (model.ThumbnailImageUrl.IsSpecified)
                ThumbnailImageUrl = model.ThumbnailImageUrl.Value;
        }

        public async Task UpdateAsync(RequestOptions options = null)
        {
            var args = new Helix.API.GetBroadcastsParams
            {
                UserIds = new[] { UserId }
            };

            var models = await Twitch.ApiClient.GetBroadcastsAsync(args, options: options).ConfigureAwait(false);
            Update(models.Data.SingleOrDefault());
        }

        public async Task<RestUser> GetUserAsync(RequestOptions options = null)
            => (await ClientHelper.GetUsersAsync(Twitch, new[] { UserId }, options: options).ConfigureAwait(false)).SingleOrDefault();

        public async Task<RestGame> GetGameAsync(RequestOptions options = null)
            => (await ClientHelper.GetGamesAsync(Twitch, new[] { GameId }, options: options).ConfigureAwait(false)).SingleOrDefault();

        public Task<RestSimpleClip> CreateClipAsync(RequestOptions options = null)
            => ClientHelper.CreateClipAsync(Twitch, Id, options);
    }
}
