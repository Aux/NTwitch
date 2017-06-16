using System.Collections.Generic;
using Model = NTwitch.Rest.API.ChatBadges;

namespace NTwitch.Rest
{
    public class RestChatBadges : IChatBadges
    {
        /// <summary> The badge that appears beside twitch admins in chat </summary>
        public IReadOnlyDictionary<string, string> Admin { get; private set; }
        /// <summary> The badge that appears beside the channel broadcaster in chat </summary>
        public IReadOnlyDictionary<string, string> Broadcaster { get; private set; }
        /// <summary> The badge that appears beside global moderators in chat </summary>
        public IReadOnlyDictionary<string, string> GlobalMod { get; private set; }
        /// <summary> The badge that appears beside channel moderators in chat </summary>
        public IReadOnlyDictionary<string, string> Mod { get; private set; }
        /// <summary> The badge that appaers beside twitch staff in chat </summary>
        public IReadOnlyDictionary<string, string> Staff { get; private set; }
        /// <summary> The badge that appears beside channel subscribers in chat </summary>
        public IReadOnlyDictionary<string, string> Subscriber { get; private set; }
        /// <summary> The badge that appears beside twitch turbo users in chat </summary>
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
