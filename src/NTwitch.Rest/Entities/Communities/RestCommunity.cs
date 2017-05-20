using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestCommunity : RestSimpleCommunity, IUpdateable
    {
        /// <summary> The id of the user that owns this community </summary>
        public ulong OwnerId { get; private set; }
        /// <summary> The summary specified for this community </summary>
        public string Summary { get; private set; }
        /// <summary> The description specified for this community </summary>
        public string Description { get; private set; }
        /// <summary> The raw html of this community's description </summary>
        public string DescriptionHtml { get; private set; }
        /// <summary> The rules specified for this community </summary>
        public string Rules { get; private set; }
        /// <summary> The raw html of this community's rules </summary>
        public string RulesHtml { get; private set; }
        /// <summary> This community's specified language </summary>
        public string Language { get; private set; }
        /// <summary> The url of this community's cover image </summary>
        public string CoverUrl { get; private set; }
        
        internal RestCommunity(BaseRestClient client, string id) 
            : base(client, id) { }

        internal new static RestCommunity Create(BaseRestClient client, Model model)
        {
            var entity = new RestCommunity(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
        {
            base.Update(model);
            OwnerId = model.OwnerId;
            Summary = model.Summary;
            Description = model.Description;
            DescriptionHtml = model.DescriptionHtml;
            Rules = model.Rules;
            RulesHtml = model.RulesHtml;
            Language = model.Language;
            CoverUrl = model.CoverImageUrl;
        }

        /// <summary> Get the most recent information for this entity </summary>
        public virtual async Task UpdateAsync()
        {
            var token = TokenHelper.GetSingleToken(Client);
            var model = await Client.RestClient.GetCommunityInternalAsync(token, Id, false);
            Update(model);
        }

        /// <summary> Get information about the user that owns this community </summary>
        public Task<RestUser> GetOwnerAsync()
            => RestHelper.GetUserAsync(Client, OwnerId);
    }
}
