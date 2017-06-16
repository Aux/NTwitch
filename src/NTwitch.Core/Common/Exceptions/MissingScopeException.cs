using System;

namespace NTwitch
{
    public class MissingScopeException : ArgumentException
    {
        public MissingScopeException(params string[] scopes)
            : base($"Missing required scopes: {string.Join(", ", scopes)}") { }
    }
}
