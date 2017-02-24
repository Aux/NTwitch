using System;

namespace NTwitch.Chat
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ChatPropertyAttribute : Attribute
    {
        public string Name { get; }
        public PropertyType Type { get; } = PropertyType.Default;

        public ChatPropertyAttribute(string name)
        {
            Name = name;
        }

        public ChatPropertyAttribute(PropertyType type)
        {
            Name = null;
            Type = type;
        }
    }

    internal enum PropertyType
    {
        Default,
        Complex,
        Content,
        ChannelName,
        Username
    }
}
