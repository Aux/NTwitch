using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetTeamsRequest : RequestBuilder
    {
        public GetTeamsRequest(PageOptions paging) 
            : base("GET", "teams")
        {
            _endpointParams.Add("limit", paging.Limit);
            _endpointParams.Add("offset", paging.Offset);
        }
    }
}
