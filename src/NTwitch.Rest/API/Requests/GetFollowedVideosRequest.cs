using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowedVideosRequest : RestRequestBuilder
    {
        public GetFollowedVideosRequest(string broadcastType, string language, string sort, PageOptions paging)
            : base("GET", "videos/followed")
        {
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
