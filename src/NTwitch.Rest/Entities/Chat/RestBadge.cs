namespace NTwitch.Rest
{
    public class RestBadge : RestEntity, IBadge
    {
        public string AlphaUrl { get; }
        public string ImageUrl { get; }
        public string Name { get; }
        public string SvgUrl { get; }
        
        internal RestBadge(TwitchRestClient client, ulong id) : base(client, id) { }
    }
}
