using NTwitch.Rest.Queue;

namespace NTwitch.Rest.API
{
    public class SetCommunityAvatarRequest : JsonRequestBuilder
    {
        public SetCommunityAvatarRequest(string communityId, string imageBase64) 
            : base("POST", $"communities/{communityId}/images/avatar", null)
        {
            SetPayload(new
            {
                avatar_image = imageBase64
            });
        }
    }
}
