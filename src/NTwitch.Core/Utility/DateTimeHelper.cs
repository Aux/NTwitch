using System;

namespace NTwitch
{
    internal static class DateTimeHelper
    {
        public static DateTime GetDateTime(string value)
            => GetDateTime(long.Parse(value));

        public static DateTime GetDateTime(long value)
        {
            var reference = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var actual = reference.AddMilliseconds(value).ToLocalTime();
            return actual;
        }
    }
}
