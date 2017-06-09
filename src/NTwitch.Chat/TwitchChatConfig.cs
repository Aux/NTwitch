using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class TwitchChatConfig : TwitchRestConfig
    {
        /// <summary> Gets or sets the provider used to generate new chat connections. </summary>
        public SocketClientProvider SocketClientProvider { get; set; } = DefaultSocketProvider.Instance;
        /// <summary> The host to connect to when making chat requests </summary>
        public string SocketHost { get; set; } = "irc.chat.twitch.tv";

        ///// <summary> Allow the authenticated user to speak in channels without the moderator permission. </summary>
        //public bool CanSpeakWithoutMod { get; set; } = false;

        /// <summary> Include message tags to several commands </summary>
        public bool RequestTags { get; } = true;
        /// <summary> Enables several Twitch-specific commands </summary>
        public bool RequestCommands { get; } = true;
        /// <summary> Include membership state event data </summary>
        public bool RequestMembership { get; } = true;
    }
}
