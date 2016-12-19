using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class TwitchPagination
    {
        public int Limit { get; set; }
        public int Page { get; set; }

        public TwitchPagination(int limit = 10, int page = 1)
        {
            Limit = limit;
            Page = page;
        }

        public override string ToString()
        {
            if (Limit < 1 || Limit > 100)
                throw new ArgumentOutOfRangeException("RequestOptions.Limit must be a number between 1 and 100.");

            if (Page < 1)
                throw new ArgumentOutOfRangeException("RequestOptions.Page must be a number greater than 0.");

            int offset = (Limit * Page) - Limit;

            return "?limit=" + Limit + "&offset=" + offset;
        }
    }
}
