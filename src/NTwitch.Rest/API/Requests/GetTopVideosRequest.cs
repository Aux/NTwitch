using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetTopVideosRequest : RequestBuilder
    {
        public GetTopVideosRequest(string game, string period, string broadcastType, string language, string sort, PageOptions paging)
            : base("GET", "videos/top")
        {
            if (game != null)
                _endpointParams.Add("game", game);
            if (period != null)
                _endpointParams.Add("period", period);
            if (broadcastType != null)
                _endpointParams.Add("broadcast_type", broadcastType);
            if (language != null)
                _endpointParams.Add("language", language);
            if (sort != null)
                _endpointParams.Add("sort", sort);

            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
        }
    }
}
