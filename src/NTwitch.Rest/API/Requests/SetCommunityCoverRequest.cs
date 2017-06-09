using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class SetCommunityCoverRequest : JsonRestRequestBuilder
    {
        public SetCommunityCoverRequest(string communityId, string imageBase64) 
            : base("POST", $"communities/{communityId}/images/cover", null)
        {
            SetPayload(new
            {
                cover_image = imageBase64
            });
        }
    }
}
