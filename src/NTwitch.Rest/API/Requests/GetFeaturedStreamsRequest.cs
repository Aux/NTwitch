using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFeaturedStreamsRequest : RequestBuilder
    {
        public GetFeaturedStreamsRequest(PageOptions paging) 
            : base("GET", "streams/featured")
        {
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
        }
    }
}
