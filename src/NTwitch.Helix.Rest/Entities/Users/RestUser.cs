using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = NTwitch.Helix.API.User;

namespace NTwitch.Helix.Rest
{
    public class RestUser : RestNamedEntity<ulong>
    {
        /// <summary> This user's name but properly capitalized </summary>
        public string Username { get; private set; }
        /// <summary> This user's special twitch ranking </summary>
        public UserType Type { get; private set; }
        /// <summary> This user's broadcaster level </summary>
        public BroadcasterType BroadcasterType { get; private set; }
        /// <summary> This user's bio description </summary>
        public string Description { get; private set; }
        /// <summary> The url of this user's profile image </summary>
        public string ProfileImageUrl { get; private set; }
        /// <summary> The url of this user's broadcast offline image </summary>
        public string OfflineImageUrl { get; private set; }
        /// <summary> The number of unique viewers for this user's broadcasts </summary>
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

        /// <summary> Update this object to the most recent information available </summary>
        public async Task UpdateAsync(RequestOptions options = null)
        {
            var models = await Twitch.ApiClient.GetUsersAsync(new[] { Id }, null, options: options).ConfigureAwait(false);
            Update(models.SingleOrDefault());
        }

        /// <summary> Change existing property values of this user </summary>
        public async Task<RestUser> ModifyAsync(string description, RequestOptions options = null)
            => await ClientHelper.ModifyMyUserAsync(Twitch, description, options).ConfigureAwait(false);

        /// <summary> Get this user's broadcast if it exists </summary>
        public async Task<RestBroadcast> GetBroadcastAsync(RequestOptions options = null)
            => (await ClientHelper.GetBroadcastsAsync(Twitch, userIds: new[] { Id }, options: options).Flatten().ConfigureAwait(false)).SingleOrDefault();

        /// <summary> Get this user's followers </summary>
        public async Task GetFollowersAsync(int amount = 20, RequestOptions options = null)
            => (await ClientHelper.GetFollowersAsync(Twitch, Id, limit: amount, options: options).Flatten().ConfigureAwait(false)).SingleOrDefault();

        /// <summary> Get this user's followings </summary>
        public async Task GetFollowingsAsync(int amount = 20, RequestOptions options = null)
            => (await ClientHelper.GetFollowersAsync(Twitch, followerId: Id, limit: amount, options: options).Flatten().ConfigureAwait(false)).SingleOrDefault();

        /// <summary> Get this user's videos </summary>
        public Task<IReadOnlyCollection<RestVideo>> GetVideosAsync(RequestOptions options = null) 
            => throw new NotImplementedException();
    }
}
