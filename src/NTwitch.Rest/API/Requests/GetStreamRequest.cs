namespace NTwitch.Rest
{
    internal class GetStreamRequest : RestRequest
    {
        public GetStreamRequest(string token, ulong channelId, StreamType type) 
            : base("GET", $"streams/{channelId}", token)
        {
            string value = null;
            switch (type)
            {
                case StreamType.All:
                    value = "all"; break;
                case StreamType.Live:
                    value = "live"; break;
                case StreamType.Playlist:
                    value = "playlist"; break;
            }

            Parameters.Add("stream_type", value);
        }
    }
}
