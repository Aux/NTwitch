using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestUser : IUser
    {
        [JsonIgnore]
        public TwitchRestClient Client { get; internal set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }
        [JsonProperty("_id")]
        public uint Id { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; internal set; }
        [JsonProperty("staff")]
        public bool IsStaff { get; internal set; }
        [JsonProperty("logo")]
        public string LogoUrl { get; internal set; }
        [JsonProperty("_links")]
        public TwitchLinks Links { get; internal set; }
        
        public Task<IChannelFollow> GetFollowAsync(string channel)
        {
            throw new NotImplementedException();
        }

        public Task<IChannelFollow> GetFollowAsync(IChannel channel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IChannel>> GetFollowsAsync(SortMode sort = SortMode.CreatedAt, SortDirection direction = SortDirection.Ascending, int limit = 10, int page = 1)
        {
            throw new NotImplementedException();
        }
        
        public override string ToString()
            => DisplayName;
    }
}
