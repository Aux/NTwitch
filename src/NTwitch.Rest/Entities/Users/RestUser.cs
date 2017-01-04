using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestUser : IEntity, IUser
    {
        public TwitchRestClient Client { get; }
        [JsonProperty("_id")]
        public ulong Id { get; internal set; }
        [JsonProperty("bio")]
        public string Bio { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }

        internal RestUser(TwitchRestClient client)
        {
            Client = client;
        }

        public static RestUser Create(TwitchRestClient client, string json)
        {
            var user = new RestUser(client);
            JsonConvert.PopulateObject(json, user);
            return user;
        }
        
        public Task<RestBlockedUser> BlockAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestChannelFollow> GetFollowAsync(ulong userid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestChannelFollow>> GetFollowsAsync(SortMode mode = SortMode.CreatedAt, SortDirection direction = SortDirection.Descending, TwitchPageOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<RestBlockedUser> UnblockAsync()
        {
            throw new NotImplementedException();
        }

        //IUser
        ITwitchClient IEntity.Client
            => Client;
        async Task<IEnumerable<IChannelFollow>> IUser.GetFollowsAsync(SortMode mode, SortDirection direction, TwitchPageOptions options)
            => await GetFollowsAsync(mode, direction, options);
        async Task<IChannelFollow> IUser.GetFollowAsync(ulong userid)
            => await GetFollowAsync(userid);
        async Task<IBlockedUser> IUser.BlockAsync()
            => await BlockAsync();
        async Task<IBlockedUser> IUser.UnblockAsync()
            => await UnblockAsync();
    }
}
