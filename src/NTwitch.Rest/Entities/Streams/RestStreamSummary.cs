namespace NTwitch.Rest
{
    public class RestStreamSummary : RestEntity, IStreamSummary
    {
        public int Channels { get; }
        public int Viewers { get; }

        public RestStreamSummary(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
