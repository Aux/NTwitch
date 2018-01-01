using Model = NTwitch.Helix.API.User;

namespace NTwitch.Helix.Rest
{
    public class RestSelfUser : RestUser
    {
        public string Email { get; private set; }

        internal RestSelfUser(BaseTwitchClient twitch, ulong id)
            : base(twitch, id) { }
        internal new static RestSelfUser Create(BaseTwitchClient twitch, Model model)
        {
            var entity = new RestSelfUser(twitch, model.Id);
            entity.Update(model);
            return entity;
        }
        internal override void Update(Model model)
        {
            base.Update(model);

            if (model.Email.IsSpecified)
                Email = model.Email.Value;
        }
    }
}
