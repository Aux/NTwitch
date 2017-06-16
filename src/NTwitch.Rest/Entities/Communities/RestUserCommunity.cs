namespace NTwitch.Rest
{
    public class RestUserCommunity : RestCommunity, IUserCommunity
    {
        /// <summary> The user used to access this community </summary>
        public IUser CurrentUser { get; private set; }

        internal RestUserCommunity(RestCommunity community)
            : base(community.Client, community.Id) { }

        internal static RestUserCommunity Create(RestCommunity community, IUser user)
        {
            var entity = new RestUserCommunity(community);
            entity.Update(community);
            return entity;
        }

        internal void Update(RestUserCommunity community)
        {
            base.Update(community);
        }
    }
}
