using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class GetCommunityRequest : RequestBuilder
    {
        public GetCommunityRequest(string communityId, bool isName) 
            : base("GET", null)
        {
            if (isName)
                _defaultEndpoint = $"communities?name={communityId}";
            else
                _defaultEndpoint = $"communities/{communityId}";
        }
    }
}
