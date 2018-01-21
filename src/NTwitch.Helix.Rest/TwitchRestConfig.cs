using NTwitch.Rest;

namespace NTwitch.Helix.Rest
{
    public class TwitchRestConfig : TwitchConfig
    {
        /// <summary> Gets or sets the provider used to generate new REST connections. </summary>
        public RestClientProvider RestClientProvider { get; set; } = DefaultRestClientProvider.Instance;
        /// <summary> The client id of the application, required when not authenticated as a user. </summary>
        public string ClientId { get; set; }
    }
}
