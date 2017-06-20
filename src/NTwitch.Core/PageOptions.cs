namespace NTwitch
{
    public class PageOptions
    {
        public static PageOptions Default => new PageOptions();

        public uint Limit { get; set; }
        public uint Offset { get; set; }

        public PageOptions(uint limit = 20, uint offset = 0)
        {
            Limit = limit;
            Offset = offset;
        }

        internal static PageOptions CreateOrClone(PageOptions options)
        {
            if (options == null)
                return new PageOptions();
            else
                return options.Clone();
        }

        public PageOptions Clone() => MemberwiseClone() as PageOptions;
    }
}
