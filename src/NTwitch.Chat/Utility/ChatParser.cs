using System;
using System.Threading.Tasks;

namespace NTwitch.Chat
{
    internal static class ChatParser
    {
        internal static Task MessageReceived(string msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }

        public static void PopulateObject(string msg, object obj)
        {
            throw new NotImplementedException();
        }

    }
}
