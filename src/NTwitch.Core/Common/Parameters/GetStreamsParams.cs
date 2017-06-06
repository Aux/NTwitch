namespace NTwitch
{
    public class GetStreamsParams
    {
        public ulong[] ChannelIds { get; set; } = null;
        public string Game { get; set; } = null;
        public string Language { get; set; } = null;
        public StreamType Type { get; set; } = StreamType.Live;
        public uint Limit { get; set; } = 25;
        public uint Offset { get; set; } = 0;
    }
}
