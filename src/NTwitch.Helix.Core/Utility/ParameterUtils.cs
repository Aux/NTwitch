using System.Linq;

namespace NTwitch.Helix
{
    internal static class ParameterUtils
    {
        public static string Parse<T>(string name, T[] values)
        {
            if (values.Count() == 0) return null;
            return string.Join("&", values.Select(value => $"{name}={value}"));
        }
    }
}
