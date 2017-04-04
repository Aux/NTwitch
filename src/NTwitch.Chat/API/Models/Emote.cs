using System.Collections.Generic;
using System.Linq;

namespace NTwitch.Chat.API
{
    internal class Emote
    {
        public uint Id { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public static IEnumerable<Emote> Parse(string msg)
        {
            var list = new List<Emote>();
            if (string.IsNullOrWhiteSpace(msg))
                return list;

            var topsplit = msg.Split(new[] { ':' }, 2);

            var id = uint.Parse(topsplit.First());
            var indices = topsplit.Last().Split(',');
            
            foreach (var index in indices)
            {
                var indexsplit = index.Split(new[] { '-' }, 2);
                var start = int.Parse(indexsplit.First());
                var end = int.Parse(indexsplit.Last());

                list.Add(new Emote()
                {
                    Id = id,
                    StartIndex = start,
                    EndIndex = end
                });
            }

            return list;
        }
    }
}
