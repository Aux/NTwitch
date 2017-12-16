using System.Reflection;

namespace NTwitch.Helix
{
    public class TwitchConfig
    {
        public const int APIVersion = 3;
        public static string Version { get; } =
            typeof(TwitchConfig).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(TwitchConfig).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        public static string UserAgent { get; } = $"NTwitch (https://github.com/auxlabs/NTwitch, v{Version})";
        public static readonly string APIUrl = "https://api.twitch.tv/helix";
        public static readonly string Encoding = "ISO-8859-1";

        public const int DefaultRequestTimeout = 15000;

        /// <summary> Gets or sets how a request should act in the case of an error, by default. </summary>
        public RetryMode DefaultRetryMode { get; set; } = RetryMode.AlwaysRetry;

        /// <summary> Gets or sets the minimum log level severity that will be sent to the Log event. </summary>
        public LogSeverity LogLevel { get; set; } = LogSeverity.Info;

        /// <summary> Gets or sets whether the initial log entry should be printed. </summary>
        internal bool DisplayInitialLog { get; set; } = true;
    }
}
