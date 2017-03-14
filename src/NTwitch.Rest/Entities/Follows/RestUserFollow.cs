using Model = NTwitch.Rest.API.UserFollow;

namespace NTwitch.Rest
{
    public class RestUserFollow : RestFollow
    {
        public RestUser User { get; private set; }

        internal RestUserFollow(BaseRestClient client) : base(client) { }

        internal static RestUserFollow Create(BaseRestClient client, Model model)
        {
            var entity = new RestUserFollow(client);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            User = new RestUser(Client, model.User.Id);
            User.Update(model.User);
            base.Update(model);
        }
    }
}
