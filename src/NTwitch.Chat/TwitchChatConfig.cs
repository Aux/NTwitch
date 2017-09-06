using NTwitch.Rest;

namespace NTwitch.Chat
{
    public class TwitchChatConfig : TwitchRestConfig
    {
        /// <summary> Gets or sets the provider used to generate new chat connections. </summary>
        public SocketClientProvider SocketClientProvider { get; set; } = DefaultSocketProvider.Instance;
        /// <summary> The host to connect to when making chat requests </summary>
        public string SocketHost { get; set; } = "irc.chat.twitch.tv";

        /// <summary> Gets or sets the provider used to cache entities. </summary>
        internal IEntityCacheProvider CacheClientProvider = DefaultEntityCacheProvider.Instance;
        /// <summary> The number of messages that should be cached for each channel. </summary>
        public int MessageCacheSize { get; set; } = 0;

        /// <summary> Gets or sets the time, in milliseconds, to wait for a connection to complete before aborting. </summary>
        public int ConnectionTimeout { get; set; } = 30000;
        /// <summary> Gets or sets the time, in milliseconds, between heartbeat requests. </summary>
        public int HeartbeatInterval { get; set; } = 300000;
    }
}
