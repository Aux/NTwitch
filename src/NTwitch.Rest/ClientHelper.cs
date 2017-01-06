using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestSelfUser> GetCurrentUserAsync(BaseTwitchClient client)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "user");
            return RestSelfUser.Create(client, json);
        }

        public static async Task<RestSelfChannel> GetCurrentChannelAsync(BaseTwitchClient client)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "channels");
            return RestSelfChannel.Create(client, json);
        }

        public static async Task<RestChannel> GetChannelAsync(BaseTwitchClient client, ulong id)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "channels/" + id);
            return RestChannel.Create(client, json);
        }
        
        public static async Task<IEnumerable<RestTopGame>> GetTopGamesAsync(BaseTwitchClient client, TwitchPageOptions options)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "limit", options?.Limit.ToString() },
                { "offset", options?.Offset.ToString() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "games/top", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("games"));
            return items.Select(x => RestTopGame.Create(client, json));
        }

        public static async Task<IEnumerable<RestIngest>> GetIngestsAsync(BaseTwitchClient client)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "ingests");
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("ingests"));
            return items.Select(x => RestIngest.Create(client, x));
        }

        public static async Task<IEnumerable<RestChannel>> FindChannelsAsync(BaseTwitchClient client, string query, TwitchPageOptions options)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "query", query },
                { "limit", options?.Limit.ToString() },
                { "offset", options?.Offset.ToString() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "search/channels", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("channels"));
            return items.Select(x => RestChannel.Create(client, x));
        }

        public static async Task<IEnumerable<RestGame>> FindGamesAsync(BaseTwitchClient client, string query, bool islive)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "query", query },
                { "live", islive ? "true" : "false" },
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "search/games", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("games"));
            return items.Select(x => RestGame.Create(client, x));
        }

        public static async Task<RestStream> GetStreamAsync(BaseTwitchClient client, ulong id, StreamType type)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "stream_type", Enum.GetName(typeof(StreamType), type).ToLower() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "streams/" + id);
            return RestStream.Create(client, json);
        }

        public static async Task<IEnumerable<RestStream>> FindStreamsAsync(BaseTwitchClient client, string query, bool hls, TwitchPageOptions options)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "query", query },
                { "hls", hls ? "true" : "false" },
                { "limit", options?.Limit.ToString() },
                { "offset", options?.Offset.ToString() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "search/streams", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("streams"));
            return items.Select(x => RestStream.Create(client, x));
        }

        public static async Task<IEnumerable<RestStream>> GetStreamsAsync(BaseTwitchClient client, string game, ulong[] channelids, string language, StreamType type, TwitchPageOptions options)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "game", game },
                { "language", language },
                { "type", Enum.GetName(typeof(StreamType), type).ToLower() },
                { "channel", string.Join(",", channelids) },
                { "limit", options?.Limit.ToString() },
                { "offset", options?.Offset.ToString() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "streams", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("streams"));
            return items.Select(x => RestStream.Create(client, x));
        }

        public static async Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseTwitchClient client, TwitchPageOptions options)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "limit", options?.Limit.ToString() },
                { "offset", options?.Offset.ToString() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "streams/featured", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("streams"));
            return items.Select(x => RestFeaturedStream.Create(client, x));
        }

        public static async Task<RestStreamSummary> GetStreamSummaryAsync(BaseTwitchClient client, string game)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "game", game }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "streams/summary", parameters);
            return RestStreamSummary.Create(client, json);
        }

        public static async Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(BaseTwitchClient client, TwitchPageOptions options)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "limit", options?.Limit.ToString() },
                { "offset", options?.Offset.ToString() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "teams", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("teams"));
            return items.Select(x => RestTeamSummary.Create(client, x));
        }

        public static async Task<RestTeam> GetTeamAsync(BaseTwitchClient client, string name)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "teams/" + name);
            return RestTeam.Create(client, json);
        }

        public static async Task<RestUser> GetUserAsync(BaseTwitchClient client, ulong id)
        {
            var json = await client.ApiClient.GetJsonAsync("GET", "users/" + id);
            return RestUser.Create(client, json);
        }

        public static async Task<IEnumerable<RestUser>> FindUsersAsync(BaseTwitchClient client, string login)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "login", login },
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "users", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("users"));
            return items.Select(x => RestUser.Create(client, x));
        }

        public static async Task<IEnumerable<RestVideo>> GetTopVideosAsync(BaseTwitchClient client, string game, VideoPeriod period, BroadcastType type, TwitchPageOptions options)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "game", game },
                { "period", Enum.GetName(typeof(VideoPeriod), period).ToLower() },
                { "broadcast_type", Enum.GetName(typeof(BroadcastType), type).ToLower() },
                { "limit", options?.Limit.ToString() },
                { "offset", options?.Offset.ToString() }
            };

            var json = await client.ApiClient.GetJsonAsync("GET", "videos/top", parameters);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchCollectionConverter("vods"));
            return items.Select(x => RestVideo.Create(client, x));
        }
    }
}
