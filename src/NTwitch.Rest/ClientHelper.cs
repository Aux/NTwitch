using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestStream> GetStreamAsync(BaseRestClient client, ulong id, StreamType type)
        {
            var request = new RequestOptions();
            request.Parameters.Add("stream_type", Enum.GetName(typeof(StreamType), type).ToLower());
            
            string json = await client.ApiClient.SendAsync("GET", "streams/" + id, request).ConfigureAwait(false);
            return RestStream.Create(client, json);
        }

        public static async Task<RestSelfUser> GetCurrentUserAsync(BaseRestClient client)
        {
            string json = await client.ApiClient.SendAsync("GET", "user").ConfigureAwait(false);
            return RestSelfUser.Create(client, json);
        }

        public static async Task<RestSelfChannel> GetCurrentChannelAsync(BaseRestClient client)
        {
            var json = await client.ApiClient.SendAsync("GET", "channels").ConfigureAwait(false);
            return RestSelfChannel.Create(client, json);
        }

        public static async Task<RestChannel> GetChannelAsync(BaseRestClient client, ulong id)
        {
            var json = await client.ApiClient.SendAsync("GET", "channels/" + id).ConfigureAwait(false);
            return RestChannel.Create(client, json);
        }

        public static async Task<IEnumerable<RestTopGame>> GetTopGamesAsync(BaseRestClient client, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", "games/top", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("games"));
            return items.Select(x => RestTopGame.Create(client, x));
        }

        public static async Task<IEnumerable<RestIngest>> GetIngestsAsync(BaseRestClient client)
        {
            string json = await client.ApiClient.SendAsync("GET", "ingests");
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("ingests"));
            return items.Select(x => RestIngest.Create(client, x));
        }

        public static async Task<IEnumerable<RestGame>> FindGamesAsync(BaseRestClient client, string query, bool islive)
        {
            var request = new RequestOptions();
            request.Parameters.Add("query", query);
            request.Parameters.Add("live", islive);

            string json = await client.ApiClient.SendAsync("GET", "search/games");
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("games"));
            return items.Select(x => RestGame.Create(client, x));
        }

        public static async Task<IEnumerable<RestChannel>> FindChannelsAsync(BaseRestClient client, string query, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("query", query);
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", "search/channels", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("games"));
            return items.Select(x => RestChannel.Create(client, x));
        }

        public static async Task<IEnumerable<RestStream>> FindStreamsAsync(BaseRestClient client, string query, bool hls, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("query", query);
            request.Parameters.Add("hls", hls);
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", "search/streams", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("streams"));
            return items.Select(x => RestStream.Create(client, x));
        }

        public static async Task<IEnumerable<RestVideo>> GetTopVideosAsync(BaseRestClient client, string game, VideoPeriod period, BroadcastType type, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("game", game);
            request.Parameters.Add("period", Enum.GetName(typeof(VideoPeriod), period).ToLower());
            request.Parameters.Add("broadcast_type", Enum.GetName(typeof(BroadcastType), type).ToLower());
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", "videos/top", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("vods"));
            return items.Select(x => RestVideo.Create(client, x));
        }

        public static async Task<RestUser> FindUserAsync(BaseRestClient client, string name)
        {
            var request = new RequestOptions();
            request.Parameters.Add("login", name);

            string json = await client.ApiClient.SendAsync("GET", "videos/top", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("vods"));
            return RestUser.Create(client, items.FirstOrDefault());
        }

        public static async Task<IEnumerable<RestStream>> GetStreamsAsync(BaseRestClient client, string game, ulong[] channelids, string language, StreamType type, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("game", game);
            request.Parameters.Add("channel", string.Join(",", channelids));
            request.Parameters.Add("language", language);
            request.Parameters.Add("type", Enum.GetName(typeof(StreamType), type).ToLower());
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", "streams", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("streams"));
            return items.Select(x => RestStream.Create(client, x));
        }

        public static async Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseRestClient client, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", "streams/featured", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("streams"));
            return items.Select(x => RestFeaturedStream.Create(client, x));
        }

        public static async Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(BaseRestClient client, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("limit", options?.Limit);
            request.Parameters.Add("offset", options?.Offset);

            string json = await client.ApiClient.SendAsync("GET", "teams", request);
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json, new TwitchConverter("teams"));
            return items.Select(x => RestTeamSummary.Create(client, x));
        }

        public static async Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            string json = await client.ApiClient.SendAsync("GET", "user/" + id);
            return RestUser.Create(client, json);
        }

        public static async Task<RestTeam> GetTeamAsync(BaseRestClient client, string name)
        {
            string json = await client.ApiClient.SendAsync("GET", "teams/" + name);
            return RestTeam.Create(client, json);
        }

        public static async Task<RestStreamSummary> GetStreamSummaryAsync(BaseRestClient client, string game)
        {
            var request = new RequestOptions();
            request.Parameters.Add("game", game);

            string json = await client.ApiClient.SendAsync("GET", "streams/summary", request);
            return RestStreamSummary.Create(client, json);
        }
    }
}
