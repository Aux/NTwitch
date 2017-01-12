using System;

namespace NTwitch
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ChatPropertyAttribute : Attribute
    {
        public string Name;

        public ChatPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}
