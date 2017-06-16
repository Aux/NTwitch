using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetFollowedClipsRequest : RestRequestBuilder
    {
        public GetFollowedClipsRequest(bool istrending, PageOptions paging)
            : base("GET", "clips/followed")
        {
            Parameters.Add("trending", istrending);
            Parameters.Add("limit", paging.Limit);
        }
    }
}
