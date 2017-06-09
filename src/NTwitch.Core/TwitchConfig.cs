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
        public static readonly string DefaultApiUrl = "https://api.twitch.tv/kraken/";
        public static readonly string DefaultChatUrl = "irc.chat.twitch.tv";

        public const int APIVersion = 5;
        public const int DefaultRequestTimeout = 15000;

        /// <summary> Gets or sets the minimum log level severity that will be sent to the Log event. </summary>
        public LogSeverity LogLevel { get; set; } = LogSeverity.Info;
    }
}
