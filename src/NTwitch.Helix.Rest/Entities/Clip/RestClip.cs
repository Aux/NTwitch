using System;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.Clip;

namespace NTwitch.Helix.Rest
{
    public class RestClip : RestSimpleClip
    {
        public string EmbedUrl { get; private set; }
        public ulong BroadcasterId { get; private set; }
        public ulong CreatorId { get; private set; }
        public ulong VideoId { get; private set; }
        public ulong GameId { get; private set; }
        public string Language { get; private set; }
        public string Title { get; private set; }
        public int ViewCount { get; private set; }
        public DateTime CreatedAt { get; private set; }
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
        
        // Get Users
        public async Task<RestUser> GetBroadcasterAsync() => throw new NotImplementedException();
        // Get Streams
        public async Task<RestBroadcast> GetBroadcastAsync() => throw new NotImplementedException();
        // Get Users
        public async Task<RestUser> GetCreatorAsync() => throw new NotImplementedException();
        // Get Videos <RestVideo>
        public async Task<RestVideo> GetVideoAsync() => throw new NotImplementedException();
        // Get Games <RestGame>
        public async Task<RestGame> GetGameAsync() => throw new NotImplementedException();
    }
}
