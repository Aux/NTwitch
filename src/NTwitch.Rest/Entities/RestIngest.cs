namespace NTwitch.Rest
{
    public class RestIngest : RestEntity, IIngest
    {
        public double Availability { get; }
        public bool IsDefault { get; }
        public string Name { get; }
        public string UrlTemplate { get; }

        public RestIngest(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
