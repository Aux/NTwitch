using System;

namespace NTwitch.Helix
{
    //Source: https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/DateTimeOffset.cs
    internal static class DateTimeUtils
    {
        private const long UnixEpochTicks = 621_355_968_000_000_000;
        private const long UnixEpochSeconds = 62_135_596_800;
        private const long UnixEpochMilliseconds = 62_135_596_800_000;

        public static DateTimeOffset FromTicks(long ticks)
            => new DateTimeOffset(ticks, TimeSpan.Zero);
        public static DateTimeOffset? FromTicks(long? ticks)
            => ticks != null ? new DateTimeOffset(ticks.Value, TimeSpan.Zero) : (DateTimeOffset?)null;

        public static DateTimeOffset FromUnixSeconds(long seconds)
        {
            long ticks = seconds * TimeSpan.TicksPerSecond + UnixEpochTicks;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }
        public static DateTimeOffset FromUnixMilliseconds(long milliseconds)
        {
            long ticks = milliseconds * TimeSpan.TicksPerMillisecond + UnixEpochTicks;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }

        public static long ToUnixSeconds(DateTimeOffset dto)
        {
            long seconds = dto.UtcDateTime.Ticks / TimeSpan.TicksPerSecond;
            return seconds - UnixEpochSeconds;
        }
        public static long ToUnixMilliseconds(DateTimeOffset dto)
        {
            long milliseconds = dto.UtcDateTime.Ticks / TimeSpan.TicksPerMillisecond;
            return milliseconds - UnixEpochMilliseconds;
        }
    }
}
