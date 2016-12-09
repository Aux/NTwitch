using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitch.Chat
{
    public partial class TwitchChatClient
    {
        public event Func<Task> Connected
        {
            add { }
            remove { }
        }

        public event Func<Task> Disconnected
        {
            add { }
            remove { }
        }

        public event Func<Task> Ready
        {
			add { }
			remove { }
        }

        public event Func<Task> MessageReceived
        {
            add { }
            remove { }
        }

        public event Func<Task> PrivateMessageReceived
        {
            add { }
            remove { }
        }

        public event Func<Task> UserNoticeReceived
        {
            add { }
            remove { }
        }

        public event Func<Task> JoinedChannel
        {
            add { }
            remove { }
        }

        public event Func<Task> LeftChannel
        {
            add { }
            remove { }
        }

        public event Func<Task> UserJoined
        {
            add { }
            remove { }
        }

        public event Func<Task> UserLeft
        {
            add { }
            remove { }
        }

        public event Func<Task> UserModeUpdated
        {
            add { }
            remove { }
        }
    }
}
