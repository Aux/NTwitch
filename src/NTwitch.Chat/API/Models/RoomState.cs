namespace NTwitch.Chat.API
{
    internal class RoomState
    {
        // 1st parameter
        public string ChannelName { get; set; }
        // broadcaster-lang tag
        public string BroadcasterLanguage { get; set; }
        // emote-only tag
        public bool IsEmotesOnly { get; set; }
        // subs-only tag
        public bool IsSubscribersOnly { get; set; }
        // followers-only tag not -1
        public bool IsFollowersOnly { get; set; }
        // followers-only tag
        public int FollowerLimit { get; set; }
        // r9k tag
        public bool IsR9kEnabled { get; set; }
        // slow tag
        public bool IsSlowEnabled { get; set; }
    }
}
