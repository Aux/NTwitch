using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Rest
{
    public sealed class Scopes
    {
        private readonly string _name;
        private readonly int _value;

        public static readonly Scopes UserRead = new Scopes(0, "user_read");
        public static readonly Scopes UserBlocksEdit = new Scopes(1, "user_blocks_edit");
        public static readonly Scopes UserBlocksRead = new Scopes(2, "user_blocks_read");
        public static readonly Scopes UserFollowsEdit = new Scopes(3, "user_follows_edit");
        public static readonly Scopes ChannelRead = new Scopes(4, "channel_read");
        public static readonly Scopes ChannelEditor = new Scopes(5, "channel_editor");
        public static readonly Scopes ChannelCommercial = new Scopes(6, "channel_commercial");
        public static readonly Scopes ChannelStream = new Scopes(7, "channel_stream");
        public static readonly Scopes ChannelSubscriptions = new Scopes(8, "channel_subscriptions");
        public static readonly Scopes UserSubscriptions = new Scopes(9, "user_subscriptions");
        public static readonly Scopes ChannelCheckSubscriptions = new Scopes(10, "channel_check_subscription");
        public static readonly Scopes ChatLogin = new Scopes(11, "chat_login");
        public static readonly Scopes ChannelFeedRead = new Scopes(12, "channel_feed_read");
        public static readonly Scopes ChannelFeedEdit = new Scopes(13, "channel_feed_edit");

        private Scopes(int value, string name)
        {
            _value = value;
            _name = name;
        }

        public override string ToString()
            => _name;

        public int ToInt32()
            => _value;
    }
}
