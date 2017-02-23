using System;

namespace NTwitch
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonPropertyAliasAttribute : Attribute
    {
        public string[] Aliases { get; }
        
        public JsonPropertyAliasAttribute(params string[] aliases)
        {
            Aliases = aliases;
        }
    }
}
