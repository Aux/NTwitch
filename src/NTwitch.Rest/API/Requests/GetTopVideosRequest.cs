using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetTopVideosRequest : RestRequestBuilder
    {
        public GetTopVideosRequest(string game, string period, string broadcastType, string language, string sort, PageOptions paging)
            : base("GET", "videos/top")
        {
            if (game != null)
                Parameters.Add("game", game);
            if (period != null)
                Parameters.Add("period", period);
            if (broadcastType != null)
                Parameters.Add("broadcast_type", broadcastType);
            if (language != null)
                Parameters.Add("language", language);
            if (sort != null)
                Parameters.Add("sort", sort);

            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
