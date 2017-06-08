using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowedVideosRequest : RequestBuilder
    {
        public GetFollowedVideosRequest(string broadcastType, string language, string sort, PageOptions paging)
            : base("GET", "videos/followed")
        {
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
