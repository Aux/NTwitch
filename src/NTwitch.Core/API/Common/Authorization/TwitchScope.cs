namespace NTwitch
{
    public sealed class TwitchScope
    {
        private readonly string _name;

        public static readonly TwitchScope UserRead = new TwitchScope("user_read");
        public static readonly TwitchScope UserBlocksEdit = new TwitchScope("user_blocks_edit");
        public static readonly TwitchScope UserBlocksRead = new TwitchScope("user_blocks_read");
        public static readonly TwitchScope UserFollowsEdit = new TwitchScope("user_follows_edit");
        public static readonly TwitchScope ChannelRead = new TwitchScope("channel_read");
        public static readonly TwitchScope ChannelEditor = new TwitchScope("channel_editor");
        public static readonly TwitchScope ChannelCommercial = new TwitchScope("channel_commercial");
        public static readonly TwitchScope ChannelStream = new TwitchScope("channel_stream");
        public static readonly TwitchScope ChannelSubscriptions = new TwitchScope("channel_subscriptions");
        public static readonly TwitchScope UserSubscriptions = new TwitchScope("user_subscriptions");
        public static readonly TwitchScope ChannelCheckSubscription = new TwitchScope("channel_check_subscription");
        public static readonly TwitchScope ChatLogin = new TwitchScope("chat_login");
        public static readonly TwitchScope ChannelFeedRead = new TwitchScope("channel_feed_read");
        public static readonly TwitchScope ChannelFeedEdit = new TwitchScope("channel_feed_edit");

        private TwitchScope(string name)
        {
            _name = name;
        }

        public override string ToString()
            => _name;
    }
}
