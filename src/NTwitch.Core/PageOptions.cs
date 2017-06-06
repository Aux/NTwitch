namespace NTwitch
{
    public class PageOptions
    {
        public static PageOptions Default => new PageOptions();

        public uint Limit { get; set; } = 20;
        public uint Offset { get; set; } = 0;

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
