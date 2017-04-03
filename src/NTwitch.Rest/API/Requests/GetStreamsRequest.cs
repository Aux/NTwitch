namespace NTwitch.Rest
{
    internal class GetStreamsRequest :RestRequest
    {
        public GetStreamsRequest(string token, GetStreamsParams options) 
            : base("GET", $"streams", token)
        {
            if (options.Channels != null)
                Parameters.Add("channels", string.Join(",", options.Channels));
            if (options.Game != null)
                Parameters.Add("game", options.Game);
            if (options.Language != null)
                Parameters.Add("language", options.Language);

            string value = null;
            switch (options.Type)
            {
                case StreamType.All:
                    value = "all"; break;
                case StreamType.Live:
                    value = "live"; break;
                case StreamType.Playlist:
                    value = "playlist"; break;
            }

            Parameters.Add("type", value);
            Parameters.Add("limit", options.Limit);
            Parameters.Add("offset", options.Offset);
        }
    }
}
