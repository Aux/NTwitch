namespace NTwitch.Rest
{
    public class RestPostEmote : RestEntity, IPostEmote
    {
        public int End { get; }
        public ulong SetId { get; }
        public int Start { get; }

        public RestPostEmote(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
