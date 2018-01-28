using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.Broadcast;

namespace NTwitch.Helix.Rest
{
    public class RestBroadcast : RestEntity<ulong>
    {
        /// <summary> The id of the user associated with this broadcast </summary>
        public ulong UserId { get; private set; }
        /// <summary> The id of the game being played </summary>
        public ulong? GameId { get; private set; }
        /// <summary> The ids of communities this broadcast is linked to </summary>
        public IReadOnlyCollection<string> CommunityIds { get; private set; }
        /// <summary> The type of broadcast </summary>
        public BroadcastType Type { get; private set; }
        /// <summary> The title of this broadcast </summary>
        public string Title { get; private set; }
        /// <summary> The current number of viewers watching this broadcast </summary>
        public int ViewerCount { get; private set; }
        /// <summary> The date and time this broadcast began </summary>
        public DateTime StartedAt { get; private set; }
        /// <summary> The language spoken in this broadcast </summary>
        public string Language { get; private set; }
        internal string ThumbnailImageUrl { get; private set; }

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
            {
                if (ulong.TryParse(model.GameId.Value, out ulong gameId))
                    GameId = gameId;
                else
                    GameId = null;
            }
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

        /// <summary> Update this object to the most recent information available </summary>
        public async Task UpdateAsync(RequestOptions options = null)
        {
            var args = new Helix.API.GetBroadcastsParams
            {
                UserIds = new[] { UserId }
            };

            var models = await Twitch.ApiClient.GetBroadcastsAsync(args, options: options).ConfigureAwait(false);
            Update(models.Data.SingleOrDefault());
        }

        /// <summary> Get the user associated with this broadcast </summary>
        public async Task<RestUser> GetUserAsync(RequestOptions options = null)
            => (await ClientHelper.GetUsersAsync(Twitch, new[] { UserId }, options: options).ConfigureAwait(false)).SingleOrDefault();
        /// <summary> Get the game being played in this broadcast </summary>
        public async Task<RestGame> GetGameAsync(RequestOptions options = null)
        {
            if (!GameId.HasValue) return null;
            return (await ClientHelper.GetGamesAsync(Twitch, new[] { GameId.Value }, options: options).ConfigureAwait(false)).SingleOrDefault();
        }
        /// <summary> Create a clip at this moment of the broadcast </summary>
        public Task<RestSimpleClip> CreateClipAsync(RequestOptions options = null)
            => ClientHelper.CreateClipAsync(Twitch, Id, options);
        /// <summary> Get the url for this broadcast's preview image </summary>
        public string GetThumbnailImageUrl(int width = 242, int height = 138)
            => ThumbnailImageUrl.Replace("{width}", width.ToString()).Replace("{height}", height.ToString());
    }
}
