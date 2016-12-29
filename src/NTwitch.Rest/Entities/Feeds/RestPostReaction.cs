namespace NTwitch.Rest
{
    public class RestPostReaction : RestEntity, IPostReaction
    {
        public int Count { get; }
        public string EmoteName { get; }
        public string Name { get; }
        public ulong[] UserIds { get; }

        public RestPostReaction(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
