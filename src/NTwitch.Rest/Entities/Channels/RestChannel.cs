using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestChannel : RestSimpleChannel, IChannel
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string Status { get; private set; }
        public string BroadcasterLanguage { get; private set; }
        public string Game { get; private set; }
        public string Language { get; private set; }
        public string LogoUrl { get; private set; }
        public string VideoBannerUrl { get; private set; }
        public string ProfileBannerUrl { get; private set; }
        public string ProfileBannerBackgroundColor { get; private set; }
        public string Url { get; private set; }
        public bool IsMature { get; private set; }
        public bool IsPartner { get; private set; }
        public uint Views { get; private set; }
        public uint Followers { get; private set; }

        internal RestChannel(BaseRestClient client, ulong id) 
            : base(client, id) { }

        internal static RestChannel Create(BaseRestClient client, Model model)
        {
            var entity = new RestChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            Status = model.Status;
            BroadcasterLanguage = model.BroadcasterLanguage;
            Game = model.Game;
            Language = model.Language;
            LogoUrl = model.LogoUrl;
            VideoBannerUrl = model.VideoBannerUrl;
            ProfileBannerUrl = model.ProfileBannerUrl;
            ProfileBannerBackgroundColor = model.ProfileBannerBackgroundColor;
            Url = model.Url;
            IsMature = model.IsMature;
            IsPartner = model.IsPartner;
            Views = model.Views;
            Followers = model.Followers;
            base.Update(model);
        }

        public override async Task UpdateAsync()
        {
            var entity = await Client.RestClient.GetChannelAsync(Id).ConfigureAwait(false);
            Update(entity);
        }
    }
}
