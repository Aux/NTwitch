using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowedClipsRequest : RequestBuilder
    {
        public GetFollowedClipsRequest(bool istrending, PageOptions paging)
            : base("GET", "clips/followed")
        {
            _endpointParams.Add("trending", istrending);
            _endpointParams.Add("limit", paging.Limit);
        }
    }
}
