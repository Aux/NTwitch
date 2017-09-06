using System;
using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestSimpleCommunity : RestNamedEntity<string>, IEquatable<RestSimpleCommunity>
    {
        /// <summary> The url to the avatar image of this community </summary>
        public string AvatarUrl { get; private set; }

        internal RestSimpleCommunity(BaseTwitchClient client, string id, string name)
            : base(client, id, name) { }

        public bool Equals(RestSimpleCommunity other)
            => Id == other.Id;
        public override string ToString()
            => Name;

        internal static RestSimpleCommunity Create(BaseTwitchClient client, Model model)
        {
            var entity = new RestSimpleCommunity(client, model.Id, model.Name);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            AvatarUrl = model.AvatarImageUrl;
        }

        internal void Update(RestSimpleCommunity community)
        {
            AvatarUrl = community.AvatarUrl;
        }
    }
}
