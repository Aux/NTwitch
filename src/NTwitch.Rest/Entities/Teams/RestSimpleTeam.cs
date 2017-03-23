using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Team;

namespace NTwitch.Rest
{
    public class RestSimpleTeam : RestEntity<ulong>
    {
        public string Background { get; private set; }
        public string BannerUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string DisplayName { get; private set; }
        public string Info { get; private set; }
        public string LogoUrl { get; private set; }
        public string Name { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public RestSimpleTeam(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestSimpleTeam Create(BaseRestClient client, Model model)
        {
            var entity = new RestSimpleTeam(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Background = model.Background;
            BannerUrl = model.BannerUrl;
            CreatedAt = model.CreatedAt;
            DisplayName = model.DisplayName;
            Info = model.Info;
            LogoUrl = model.LogoUrl;
            Name = model.Name;
            UpdatedAt = model.UpdatedAt;
        }

        public async Task UpdateAsync()
        {
            var model = await Client.RestClient.GetTeamAsync(Name).ConfigureAwait(false);
            Update(model);
        }
    }
}
