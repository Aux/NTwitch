using System.Collections.Generic;
using Model = NTwitch.Rest.API.ChatBadges;

namespace NTwitch.Rest
{
    public class RestChatBadges
    {
        public IReadOnlyDictionary<string, string> Admin { get; private set; }
        public IReadOnlyDictionary<string, string> Broadcaster { get; private set; }
        public IReadOnlyDictionary<string, string> GlobalMod { get; private set; }
        public IReadOnlyDictionary<string, string> Mod { get; private set; }
        public IReadOnlyDictionary<string, string> Staff { get; private set; }
        public IReadOnlyDictionary<string, string> Subscriber { get; private set; }
        public IReadOnlyDictionary<string, string> Turbo { get; private set; }

        internal RestChatBadges() { }

        internal static RestChatBadges Create(Model model)
        {
            var entity = new RestChatBadges();
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Admin = model.Admin;
            Broadcaster = model.Broadcaster;
            GlobalMod = model.GlobalMod;
            Mod = model.Mod;
            Staff = model.Staff;
            Subscriber = model.Subscriber;
            Turbo = model.Turbo;
        }
    }
}
