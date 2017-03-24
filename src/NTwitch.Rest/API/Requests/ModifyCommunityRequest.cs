using Newtonsoft.Json;

namespace NTwitch.Rest
{
    internal class ModifyCommunityRequest : RestRequest
    {
        public ModifyCommunityRequest(string communityId, ModifyCommunityParams changes) 
            : base("PUT", $"communities/{communityId}")
        {
            JsonBody = JsonConvert.SerializeObject(changes);
        }
    }
}
