using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestCommunity : RestEntity<string>
    {
        public ulong OwnerId { get; private set; }
        public string Name { get; private set; }
        public string Summary { get; private set; }
        public string Description { get; private set; }
        public string DescriptionHtml { get; private set; }
        public string Rules { get; private set; }
        public string RulesHtml { get; private set; }
        public string Language { get; private set; }
        public string AvatarUrl { get; private set; }
        public string CoverUrl { get; private set; }

        internal RestCommunity(BaseRestClient client, string id) 
            : base(client, id) { }

        internal static RestCommunity Create(BaseRestClient client, Model model)
        {
            var entity = new RestCommunity(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            OwnerId = model.OwnerId;
            Name = model.Name;
            Summary = model.Summary;
            Description = model.Description;
            DescriptionHtml = model.DescriptionHtml;
            Rules = model.Rules;
            RulesHtml = model.RulesHtml;
            Language = model.Language;
            AvatarUrl = model.AvatarImageUrl;
            CoverUrl = model.CoverImageUrl;
        }

        public virtual Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RestUser> GetOwnerAsync()
            => ClientHelper.GetUserAsync(Client, OwnerId);
        //public Task ModifyAsync(Action<ModifyCommunityParams> properties)
        //    => ClientHelper.ModifyChannelAsync(properties);
    }
}
