using Newtonsoft.Json;
using NTwitch.Rest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Pubsub
{
    public class PubsubUser : PubsubEntity, IUser
    {
        [JsonProperty("id")]
        public ulong Id { get; internal set; }
        [JsonProperty("login")]
        public string Name { get; internal set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }
        [JsonProperty("color")]
        public string Color { get; internal set; }
        [JsonProperty("badges")]
        public object[] Badges { get; internal set; }

        public PubsubUser(TwitchPubsubClient client) : base(client) { }

        public Task<IBlockedUser> BlockAsync()
            => UserHelper.BlockAsync(this, Client, Id);
        public Task<IChannelFollow> GetFollowAsync(uint channelId)
            => UserHelper.GetFollowAsync(this, Client, channelId);
        public Task<IEnumerable<IChannelFollow>> GetFollowsAsync()
            => GetFollowsAsync(SortMode.CreatedAt);
        public Task<IEnumerable<IChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, bool ascending = false, PageOptions options = null)
            => UserHelper.GetFollowsAsync(this, Client, mode, ascending, options);
        public Task UnblockAsync()
            => UserHelper.UnblockAsync(this, Client, Id);
    }
}
