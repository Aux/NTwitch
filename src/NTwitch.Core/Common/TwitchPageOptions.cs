using System;

namespace NTwitch
{
    public class PageOptions
    {
        public int Limit { get; set; }
        public int Page { get; set; }
        public int Offset
            => (Limit * Page) - Limit;

        public PageOptions(int limit = 10, int page = 1)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("Limit must be a number between 1 and 100.");
            else
                Limit = limit;

            if (page < 1)
                throw new ArgumentOutOfRangeException("Page must be a number greater than 0.");
            else
                Page = page;
        }
    }
}
