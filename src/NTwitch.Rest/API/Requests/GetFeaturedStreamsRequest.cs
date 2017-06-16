using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFeaturedStreamsRequest : RestRequestBuilder
    {
        public GetFeaturedStreamsRequest(PageOptions paging) 
            : base("GET", "streams/featured")
        {
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
