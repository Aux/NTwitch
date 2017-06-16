using Model = NTwitch.Rest.API.CommunityPermissions;

namespace NTwitch.Rest
{
    public class RestCommunityPermissions : ICommunityPermissions
    {
        /// <summary> True if the authorized user can ban users from this community </summary>
        public bool CanBan { get; private set; }
        /// <summary> True if the authorized user can timeout users from this community </summary>
        public bool CanTimeout { get; private set; }
        /// <summary> True if the authorized user can modify properties of this community </summary>
        public bool CanEdit { get; private set; }

        internal RestCommunityPermissions() { }

        internal static RestCommunityPermissions Create(Model model)
        {
            var entity = new RestCommunityPermissions();
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            CanBan = model.CanBan;
            CanTimeout = model.CanTimeout;
            CanEdit = model.CanEdit;
        }
    }
}
