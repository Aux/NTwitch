namespace NTwitch.Pubsub
{
    internal class PubsubTopic
    {
        public string Id { get; }
        public string Type { get; }
        public string Name { get; }

        public PubsubTopic(string topic)
        {
            var parts = topic.Split('-', '.');

            Type = parts[0];
            Name = parts[1];
            Id = parts[2];
        }

        public override string ToString()
            => $"{Type}-{Name}.{Id}";
    }
}
