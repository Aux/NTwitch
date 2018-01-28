using System;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.Clip;

namespace NTwitch.Helix.Rest
{
    public class RestClip : RestSimpleClip
    {
        /// <summary> The url used to embed this clip in a webpage </summary>
        public string EmbedUrl { get; private set; }
        /// <summary> The id of the user broadcasting this clip </summary>
        public ulong BroadcasterId { get; private set; }
        /// <summary> The id of the user who created this clip </summary>
        public ulong CreatorId { get; private set; }
        /// <summary> The id of the video this clip was created from </summary>
        public ulong VideoId { get; private set; }
        /// <summary> The game being played in this clip </summary>
        public ulong GameId { get; private set; }
        /// <summary> The language spoken in this clip </summary>
        public string Language { get; private set; }
        /// <summary> The title of this clip </summary>
        public string Title { get; private set; }
        /// <summary> The number of views this clip has received </summary>
        public int ViewCount { get; private set; }
        /// <summary> The date and time this clip was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The url for this clip's preview image </summary>
        public string ThumbnailImageUrl { get; private set; }
        
        internal RestClip(BaseTwitchClient twitch, string id) 
            : base(twitch, id) { }
        internal new static RestClip Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestClip(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal override void Update(Model model)
        {
            base.Update(model);

            if (model.EmbedUrl.IsSpecified)
                EmbedUrl = model.EmbedUrl.Value;
            if (model.BroadcasterId.IsSpecified)
                BroadcasterId = model.BroadcasterId.Value;
            if (model.CreatorId.IsSpecified)
                CreatorId = model.CreatorId.Value;
            if (model.VideoId.IsSpecified)
                VideoId = model.VideoId.Value;
            if (model.GameId.IsSpecified)
                GameId = model.GameId.Value;
            if (model.Language.IsSpecified)
                Language = model.Language.Value;
            if (model.Title.IsSpecified)
                Title = model.Title.Value;
            if (model.ViewCount.IsSpecified)
                ViewCount = model.ViewCount.Value;
            if (model.CreatedAt.IsSpecified)
                CreatedAt = model.CreatedAt.Value;
            if (model.ThumbnailImageUrl.IsSpecified)
                ThumbnailImageUrl = model.ThumbnailImageUrl.Value;
        }

        /// <summary> Update this object to the most recent information available </summary>
        public async Task UpdateAsync(RequestOptions options = null)
        {
            var model = await Twitch.ApiClient.GetClipAsync(Id, options).ConfigureAwait(false);
            Update(model);
        }

        /// <summary> Get the broadcaster associated with this clip </summary>
        public async Task<RestUser> GetBroadcasterAsync(RequestOptions options = null)
            => (await ClientHelper.GetUsersAsync(Twitch, new[] { BroadcasterId }, options: options).ConfigureAwait(false)).SingleOrDefault();
        /// <summary> Get the user who created this clip </summary>
        public async Task<RestUser> GetCreatorAsync(RequestOptions options = null)
            => (await ClientHelper.GetUsersAsync(Twitch, new[] { CreatorId }, options: options).ConfigureAwait(false)).SingleOrDefault();
        /// <summary> Get the video associated with this clip </summary>
        public Task<RestVideo> GetVideoAsync(RequestOptions options = null) 
            => throw new NotImplementedException();
        /// <summary> Get the game being played in this clip </summary>
        public async Task<RestGame> GetGameAsync(RequestOptions options = null)
            => (await ClientHelper.GetGamesAsync(Twitch, new[] { GameId }, options: options).ConfigureAwait(false)).SingleOrDefault();
    }
}
