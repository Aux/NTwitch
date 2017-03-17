namespace NTwitch.Rest
{
    internal class ModifyChannelRequest : RestRequest
    {
        public ModifyChannelRequest(ulong channelId, ModifyChannelParams changes)
            : base("PUT", $"channels/{channelId}", null, GetBodyString(changes)) { }
    }
}
