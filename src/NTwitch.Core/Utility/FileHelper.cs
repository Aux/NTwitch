using System;
using System.IO;

namespace NTwitch
{
    internal static class FileHelper
    {
        public static string GetBase64String(Stream stream)
        {
            byte[] bytes;
            byte[] buffer = new byte[16 * 1024];
            using (var memory = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    memory.Write(buffer, 0, read);
                bytes = memory.ToArray();
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
