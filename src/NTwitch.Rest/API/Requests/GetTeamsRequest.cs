using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetTeamsRequest : RestRequestBuilder
    {
        public GetTeamsRequest(PageOptions paging) 
            : base("GET", "teams")
        {
            Parameters.Add("limit", paging.Limit);
            Parameters.Add("offset", paging.Offset);
        }
    }
}
