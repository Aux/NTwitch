using System.Reflection;

namespace NTwitch
{
    public class TwitchConfig
    {
        public static string Version { get; } =
            typeof(TwitchConfig).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(TwitchConfig).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        public static string UserAgent { get; } = $"NTwitchApp (https://github.com/Aux/NTwitch, v{Version})";
        public static readonly string APIUrl = "https://api.twitch.tv/kraken/";
        public static readonly string TCPChatUrl = "irc.chat.twitch.tv";
        public static readonly string EmoteCdnUrl = "https://static-cdn.jtvnw.net/bits/";

        public const int APIVersion = 5;
        public const int DefaultRequestTimeout = 15000;

        /// <summary> Gets or sets how a request should act in the case of an error, by default. </summary>
        public RetryMode DefaultRetryMode { get; set; } = RetryMode.AlwaysRetry;

        /// <summary> Gets or sets the minimum log level severity that will be sent to the Log event. </summary>
        public LogSeverity LogLevel { get; set; } = LogSeverity.Info;

        /// <summary> Gets or sets whether the initial log entry should be printed. </summary>
        internal bool DisplayInitialLog { get; set; } = true;
    }
}
