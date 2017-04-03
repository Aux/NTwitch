namespace NTwitch.Rest
{
    internal class GetFollowedStreamsRequest : RestRequest
    {
        public GetFollowedStreamsRequest(string token, StreamType type, uint limit, uint offset)
            : base("GET", "streams/followed", token)
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

            Parameters.Add("type", value);
            Parameters.Add("limit", limit);
            Parameters.Add("offset", offset);
        }
    }
}
