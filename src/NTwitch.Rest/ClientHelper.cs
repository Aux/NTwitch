using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static async Task<RestStream> GetStreamAsync(BaseRestClient client, ulong id, StreamType? type)
        {
            var request = new RequestOptions();
            if (type != null)
                request.Parameters.Add("stream_type", type.ToString().ToLower());
            
            string json = await client.ApiClient.SendAsync("GET", $"streams/{id}", request).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<RestStream>(json, new TwitchEntityConverter(client, "stream"));
        }

        public static async Task<RestSelfUser> GetCurrentUserAsync(BaseRestClient client)
        {
            string json = await client.ApiClient.SendAsync("GET", "user").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<RestSelfUser>(json, new TwitchEntityConverter(client));
        }

        public static async Task<RestSelfChannel> GetCurrentChannelAsync(BaseRestClient client)
        {
            var json = await client.ApiClient.SendAsync("GET", "channel").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<RestSelfChannel>(json, new TwitchEntityConverter(client));
        }

        public static async Task<RestChannel> GetChannelAsync(BaseRestClient client, ulong id)
        {
            var json = await client.ApiClient.SendAsync("GET", $"channels/{id}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<RestChannel>(json, new TwitchEntityConverter(client));
        }

        public static async Task<IEnumerable<RestTopGame>> GetTopGamesAsync(BaseRestClient client, PageOptions options)
        {
            var request = new RequestOptions();
            if (options != null)
            {
                request.Parameters.Add("limit", options?.Limit);
                request.Parameters.Add("offset", options?.Offset);
            }

            string json = await client.ApiClient.SendAsync("GET", "games/top", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestTopGame>>(json, new TwitchCollectionConverter(client));
        }

        //
        ////  Issue #8
        ////  Properties don't fill as intended.
        //
        public static async Task<IEnumerable<RestIngest>> GetIngestsAsync(BaseRestClient client)
        {
            string json = await client.ApiClient.SendAsync("GET", "ingests");
            return JsonConvert.DeserializeObject<IEnumerable<RestIngest>>(json, new TwitchCollectionConverter(client, "ingests"));
        }

        public static async Task<IEnumerable<RestGame>> FindGamesAsync(BaseRestClient client, string query, bool? islive)
        {
            var request = new RequestOptions();
            request.Parameters.Add("query", query);
            if (islive != null)
                request.Parameters.Add("live", islive);

            string json = await client.ApiClient.SendAsync("GET", "search/games", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestGame>>(json, new TwitchCollectionConverter(client, "games"));
        }

        public static async Task<IEnumerable<RestChannel>> FindChannelsAsync(BaseRestClient client, string query, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("query", query);
            if (options != null)
            {
                request.Parameters.Add("limit", options?.Limit);
                request.Parameters.Add("offset", options?.Offset);
            }
            
            string json = await client.ApiClient.SendAsync("GET", "search/channels", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestChannel>>(json, new TwitchCollectionConverter(client, "channels"));
        }

        public static async Task<IEnumerable<RestStream>> FindStreamsAsync(BaseRestClient client, string query, bool? hls, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("query", query);
            if (hls != null)
                request.Parameters.Add("hls", hls);
            if (options != null)
            {
                request.Parameters.Add("limit", options?.Limit);
                request.Parameters.Add("offset", options?.Offset);
            }

            string json = await client.ApiClient.SendAsync("GET", "search/streams", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestStream>>(json, new TwitchCollectionConverter(client, "streams"));
        }

        public static async Task<IEnumerable<RestVideo>> GetTopVideosAsync(BaseRestClient client, string game, VideoPeriod? period, BroadcastType? type, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("game", game);
            if (period != null)
                request.Parameters.Add("period", period.ToString().ToLower());
            if (type != null)
                request.Parameters.Add("broadcast_type", type.ToString().ToLower());
            if (options != null)
            {
                request.Parameters.Add("limit", options?.Limit);
                request.Parameters.Add("offset", options?.Offset);
            }

            string json = await client.ApiClient.SendAsync("GET", "videos/top", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestVideo>>(json, new TwitchCollectionConverter(client, "vods"));
        }

        public static async Task<RestUser> FindUserAsync(BaseRestClient client, string name)
        {
            var request = new RequestOptions();
            request.Parameters.Add("login", name);

            string json = await client.ApiClient.SendAsync("GET", "users", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestUser>>(json, new TwitchCollectionConverter(client, "users")).First();
        }

        //
        ////  Issue #9
        ////  Properties don't fill as intended.
        //
        public static async Task<IEnumerable<RestStream>> GetStreamsAsync(BaseRestClient client, string game, uint[] channelids, string language, StreamType? type, PageOptions options)
        {
            var request = new RequestOptions();
            request.Parameters.Add("game", game);
            if (channelids != null)
                request.Parameters.Add("channel", string.Join(",", channelids));
            if (language != null)
                request.Parameters.Add("language", language);
            if (type != null)
            request.Parameters.Add("type", type.ToString().ToLower());
            if (options != null)
            {
                request.Parameters.Add("limit", options?.Limit);
                request.Parameters.Add("offset", options?.Offset);
            }

            string json = await client.ApiClient.SendAsync("GET", "streams", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestStream>>(json, new TwitchCollectionConverter(client, "streams"));
        }
        
        public static async Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseRestClient client, PageOptions options)
        {
            var request = new RequestOptions();
            if (options != null)
            {
                request.Parameters.Add("limit", options?.Limit);
                request.Parameters.Add("offset", options?.Offset);
            }

            string json = await client.ApiClient.SendAsync("GET", "streams/featured", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestFeaturedStream>>(json, new TwitchCollectionConverter(client, "featured"));
        }

        public static async Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(BaseRestClient client, PageOptions options)
        {
            var request = new RequestOptions();
            if (options != null)
            {
                request.Parameters.Add("limit", options?.Limit);
                request.Parameters.Add("offset", options?.Offset);
            }

            string json = await client.ApiClient.SendAsync("GET", "teams", request);
            return JsonConvert.DeserializeObject<IEnumerable<RestTeamSummary>>(json, new TwitchCollectionConverter(client, "teams"));
        }

        public static async Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            string json = await client.ApiClient.SendAsync("GET", $"users/{id}");
            return JsonConvert.DeserializeObject<RestUser>(json, new TwitchEntityConverter(client));
        }
        
        public static async Task<RestTeam> GetTeamAsync(BaseRestClient client, string name)
        {
            string json = await client.ApiClient.SendAsync("GET", $"teams/{name}");
            return JsonConvert.DeserializeObject<RestTeam>(json, new TwitchEntityConverter(client));
        }

        public static async Task<RestStreamSummary> GetStreamSummaryAsync(BaseRestClient client, string game)
        {
            var request = new RequestOptions();
            request.Parameters.Add("game", game);

            string json = await client.ApiClient.SendAsync("GET", "streams/summary", request);
            return JsonConvert.DeserializeObject<RestStreamSummary>(json, new TwitchEntityConverter(client));
        }
    }
}
