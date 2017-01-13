using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class ClientHelper
    {
        public static Task<RestStream> GetStreamAsync(BaseRestClient client, ulong id, StreamType all)
        {
            throw new NotImplementedException();
        }

        public static Task<RestSelfUser> GetCurrentUserAsync(BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        public static Task<RestSelfChannel> GetCurrentChannelAsync(BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        public static Task<RestChannel> GetChannelAsync(BaseRestClient client, ulong id)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestTopGame>> GetTopGamesAsync(BaseRestClient client, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestIngest>> GetIngestsAsync(BaseRestClient client)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestGame>> FindGamesAsync(BaseRestClient client, string query, bool islive)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestChannel>> FindChannelsAsync(BaseRestClient client, string query, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestStream>> FindStreamsAsync(BaseRestClient client, string query, bool hls, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestVideo>> GetTopVideosAsync(BaseRestClient client, string game, VideoPeriod period, BroadcastType type, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestUser>> FindUsersAsync(BaseRestClient client, string name)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestStream>> GetStreamsAsync(BaseRestClient client, string game, ulong[] channelids, string language, StreamType type, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestFeaturedStream>> GetFeaturedStreamsAsync(BaseRestClient client, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<RestTeamSummary>> GetTeamsAsync(BaseRestClient client, PageOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task<RestUser> GetUserAsync(BaseRestClient client, ulong id)
        {
            throw new NotImplementedException();
        }

        public static Task<RestTeam> GetTeamAsync(BaseRestClient client, string name)
        {
            throw new NotImplementedException();
        }

        public static Task<RestStreamSummary> GetStreamSummaryAsync(BaseRestClient client, string game)
        {
            throw new NotImplementedException();
        }
    }
}
