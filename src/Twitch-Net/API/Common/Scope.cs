namespace Twitch
{
    public sealed class Scope
    {
        private readonly string _name;

        public static readonly Scope UserRead = new Scope("user_read");
        public static readonly Scope UserBlocksEdit = new Scope("user_blocks_edit");
        public static readonly Scope UserBlocksRead = new Scope("user_blocks_read");
        public static readonly Scope UserFollowsEdit = new Scope("user_follows_edit");
        public static readonly Scope ChannelRead = new Scope("channel_read");
        public static readonly Scope ChannelEditor = new Scope("channel_editor");
        public static readonly Scope ChannelCommercial = new Scope("channel_commercial");
        public static readonly Scope ChannelStream = new Scope("channel_stream");
        public static readonly Scope ChannelSubscriptions = new Scope("channel_subscriptions");
        public static readonly Scope UserSubscriptions = new Scope("user_subscriptions");
        public static readonly Scope ChannelCheckSubscription = new Scope("channel_check_subscription");
        public static readonly Scope ChatLogin = new Scope("chat_login");
        public static readonly Scope ChannelFeedRead = new Scope("channel_feed_read");
        public static readonly Scope ChannelFeedEdit = new Scope("channel_feed_edit");

        private Scope(string name)
        {
            _name = name;
        }

        public override string ToString()
            => _name;
    }
}
