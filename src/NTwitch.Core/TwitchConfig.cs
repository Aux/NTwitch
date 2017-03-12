using System.Reflection;

namespace NTwitch
{
    public class TwitchConfig
    {
        public static string Version { get; } =
            typeof(TwitchConfig).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(TwitchConfig).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";
        public static string UserAgent { get; } = $"NTwitch Application (https://github.com/Aux/NTwitch, v{Version})";
    }
}
