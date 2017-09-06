using System;
using Model = NTwitch.Rest.API.Team;

namespace NTwitch.Rest
{
    public class RestSimpleTeam : RestNamedEntity<ulong>, IEquatable<RestSimpleTeam>
    {
        /// <summary> The hexadecimal color of this team's background </summary>
        public string Background { get; private set; }
        /// <summary> The url to this team's banner image </summary>
        public string BannerUrl { get; private set; }
        /// <summary> The date and time this team was created </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary> The display name of this team </summary>
        public string DisplayName { get; private set; }
        /// <summary> The description provided for this channel </summary>
        public string Info { get; private set; }
        /// <summary> The url to this team's logo image </summary>
        public string LogoUrl { get; private set; }
        /// <summary> The date and time this team was last updated </summary>
        public DateTime UpdatedAt { get; private set; }

        internal RestSimpleTeam(BaseTwitchClient client, ulong id, string name) 
            : base(client, id, name) { }
        
        public bool Equals(RestSimpleTeam other)
            => Id == other.Id;
        public override string ToString()
            => Name;

        internal static RestSimpleTeam Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestSimpleTeam(client, model.Id, model.Name);
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
            UpdatedAt = model.UpdatedAt;
        }
    }
}
