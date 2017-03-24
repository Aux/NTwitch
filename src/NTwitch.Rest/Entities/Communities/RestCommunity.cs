using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestCommunity : RestSimpleCommunity
    {
        public ulong OwnerId { get; private set; }
        public string Summary { get; private set; }
        public string Description { get; private set; }
        public string DescriptionHtml { get; private set; }
        public string Rules { get; private set; }
        public string RulesHtml { get; private set; }
        public string Language { get; private set; }
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

        public virtual async Task UpdateAsync()
        {
            var entity = await Client.RestClient.GetCommunityAsync(Id, false).ConfigureAwait(false);
            Update(entity);
        }

        public Task<RestUser> GetOwnerAsync()
            => ClientHelper.GetUserAsync(Client, OwnerId);
    }
}
