using System;

namespace NTwitch.Chat
{
    public class ChatNamedEntity<TId> : ChatEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary> The unique name sometimes used to identify this entity </summary>
        public string Name { get; }

        public ChatNamedEntity(TwitchChatClient client, TId id, string name)
            : base(client, id)
        {
            Name = name;
        }

        public override string ToString()
            => Name;
    }
}
