using System;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Channel;

namespace NTwitch.Rest
{
    public class RestChannel : RestSimpleChannel, IChannel, IUpdateable
    {
        /// <summary> The date and time this channel was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The date and time this channel was last updated </summary>
        public DateTime UpdatedAt { get; private set; }
        /// <summary> The channel's stream title </summary>
        public string Status { get; private set; }
        /// <summary> The language this channel is broadcasting in </summary>
        public string BroadcasterLanguage { get; private set; }
        /// <summary> The name of the game this channel is broadcasting </summary>
        public string Game { get; private set; }
        /// <summary> The language this channel is broadcasting in </summary>
        public string Language { get; private set; }
        /// <summary> The url to this channel's logo image </summary>
        public string LogoUrl { get; private set; }
        /// <summary> The url to this channel's video banner image </summary>
        public string VideoBannerUrl { get; private set; }
        /// <summary> The url to this channel's profile banner image </summary>
        public string ProfileBannerUrl { get; private set; }
        /// <summary> The hexadecimal color of this channel's profile background </summary>
        public string ProfileBannerBackgroundColor { get; private set; }
        /// <summary> The url to this channel's main page </summary>
        public string Url { get; private set; }
        /// <summary> True if this channel is flagged as mature </summary>
        public bool IsMature { get; private set; }
        /// <summary> True if this channel is partered with twitch </summary>
        public bool IsPartner { get; private set; }
        /// <summary> The number of current viewers on this channel </summary>
        public uint Views { get; private set; }
        /// <summary> The number of followers on this channel </summary>
        public uint Followers { get; private set; }

        internal RestChannel(BaseTwitchClient client, ulong id) 
            : base(client, id) { }

        internal new static RestChannel Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestChannel(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal override void Update(Model model)
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
            IsMature = model.IsMature == true;
            IsPartner = model.IsPartner == true;
            Views = model.Views;
            Followers = model.Followers;
            base.Update(model);
        }

        /// <summary> Get the most recent information for this entity </summary>
        public virtual async Task UpdateAsync()
        {
            var model = await Client.ApiClient.GetChannelAsync(Id, null).ConfigureAwait(false);
            Update(model);
        }
    }
}
