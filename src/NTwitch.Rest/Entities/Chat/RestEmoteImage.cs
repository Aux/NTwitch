namespace NTwitch.Rest
{
    public class RestEmoteImage : RestEntity, IEmoteImage
    {
        public int Height { get; }
        public string ImageUrl { get; }
        public ulong SetId { get; }
        public int Width { get; }

        internal RestEmoteImage(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
