using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public sealed class Scopes
    {
        private readonly string _name;

        public static readonly Scopes UserRead = new Scopes("user_read");
        public static readonly Scopes UserBlocksEdit = new Scopes("user_blocks_edit");
        public static readonly Scopes UserBlocksRead = new Scopes("user_blocks_read");
        public static readonly Scopes UserFollowsEdit = new Scopes("user_follows_edit");
        public static readonly Scopes ChannelRead = new Scopes("channel_read");
        public static readonly Scopes ChannelEditor = new Scopes("channel_editor");
        public static readonly Scopes ChannelCommercial = new Scopes("channel_commercial");
        public static readonly Scopes ChannelStream = new Scopes("channel_stream");
        public static readonly Scopes ChannelSubscriptions = new Scopes("channel_subscriptions");
        public static readonly Scopes UserSubscriptions = new Scopes("user_subscriptions");
        public static readonly Scopes ChannelCheckSubscriptions = new Scopes("channel_check_subscription");
        public static readonly Scopes ChatLogin = new Scopes("chat_login");
        public static readonly Scopes ChannelFeedRead = new Scopes("channel_feed_read");
        public static readonly Scopes ChannelFeedEdit = new Scopes("channel_feed_edit");

        private Scopes(string name)
        {
            _name = name;
        }

        public override string ToString()
            => _name;
    }
}
