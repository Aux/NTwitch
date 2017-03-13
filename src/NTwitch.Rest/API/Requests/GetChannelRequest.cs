namespace NTwitch.Rest
{
    internal class GetChannelRequest : RestRequest
    {
        public GetChannelRequest(ulong id) : base("GET", $"channels/{id}") { }
    }
}