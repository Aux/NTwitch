namespace NTwitch.Rest
{
    public class RestChannelSummary : RestEntity, IChannelSummary
    {
        public string DisplayName { get; }
        public string Name { get; }

        public RestChannelSummary(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
