using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.User;

namespace NTwitch.Helix.Rest
{
    public class RestUser : RestNamedEntity<ulong>
    {
        public string Username { get; private set; }
        public UserType Type { get; private set; }
        public string BroadcasterType { get; private set; }
        public string Description { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public string OfflineImageUrl { get; private set; }
        public string ViewCount { get; private set; }

        internal RestUser(BaseTwitchClient twitch, ulong id, string name)
            : base(twitch, id, name) { }
        internal static RestUser Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestUser(twitch, model.Id, model.Name);
            entity.Update(model);
            return entity;
        }
        internal virtual void Update(Model model)
        {
            if (model.Username.IsSpecified)
                Username = model.Username.Value;
            if (model.Type.IsSpecified)
                Type = model.Type.Value;
            if (model.BroadcasterType.IsSpecified)
                BroadcasterType = model.BroadcasterType.Value;
            if (model.Description.IsSpecified)
                Description = model.Description.Value;
            if (model.ProfileImageUrl.IsSpecified)
                ProfileImageUrl = model.ProfileImageUrl.Value;
            if (model.OfflineImageUrl.IsSpecified)
                OfflineImageUrl = model.OfflineImageUrl.Value;
            if (model.ViewCount.IsSpecified)
                ViewCount = model.ViewCount.Value;
        }

        public async Task UpdateAsync(RequestOptions options = null)
        {
            var models = await Twitch.ApiClient.GetUsersAsync(new[] { Id }, null, options: options).ConfigureAwait(false);
            Update(models.SingleOrDefault());
        }

        public async Task<RestUser> ModifyAsync(string description, RequestOptions options = null)
            => await ClientHelper.ModifyMyUserAsync(Twitch, description, options).ConfigureAwait(false);

        public async Task<RestBroadcast> GetBroadcastAsync(RequestOptions options = null)
            => (await ClientHelper.GetBroadcastsAsync(Twitch, userIds: new[] { Id }, options: options).Flatten().ConfigureAwait(false)).SingleOrDefault();

        public async Task GetFollowersAsync(int limit = 20, RequestOptions options = null)
            => (await ClientHelper.GetFollowersAsync(Twitch, Id, options: options).Flatten().ConfigureAwait(false)).SingleOrDefault();

        public async Task GetFollowingAsync(RequestOptions options = null)
            => (await ClientHelper.GetFollowersAsync(Twitch, followerId: Id, options: options).Flatten().ConfigureAwait(false)).SingleOrDefault();

        public Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(RequestOptions options = null) 
            => throw new NotImplementedException();
    }
}
