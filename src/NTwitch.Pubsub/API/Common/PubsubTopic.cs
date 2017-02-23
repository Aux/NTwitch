namespace NTwitch.Pubsub
{
    public class PubsubTopic
    {
        public string Id { get; }
        public string Type { get; }

        /// <summary> Create a topic object from an existing well-formatting topic string. </summary>
        public PubsubTopic(string topic)
        {
            var parts = topic.Split('-', '.');

            Type = parts[0];
            Id = parts[2];
        }

        /// <summary> Create a topic object. </summary>
        /// <param name="type"> The type of object for this topic. </param>
        /// <param name="name"> The name of this event. </param>
        /// <param name="id"> The id of the relevant event object. </param>
        public PubsubTopic(string type, ulong id)
        {
            Type = type;
            Id = id.ToString();
        }

        public override string ToString()
            => $"{Type}.{Id}";
    }
}
