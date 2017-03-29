using System;
using Model = NTwitch.Pubsub.API.WhisperMessage;

namespace NTwitch.Pubsub
{
    public class PubsubWhisperMessage : PubsubEntity<ulong>
    {
        public DateTime Timestamp { get; private set; }
        public PubsubWhisperUser Author { get; private set; }
        public PubsubWhisperUser Self { get; private set; }
        public string Type { get; private set; }
        public string ThreadId { get; private set; }
        public string Content { get; private set; }

        internal PubsubWhisperMessage(BasePubsubClient client, ulong id)
            : base(client, id) { }

        internal static PubsubWhisperMessage Create(BasePubsubClient client, Model model)
        {
            var entity = new PubsubWhisperMessage(client, model.Data.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            var author = PubsubWhisperUser.Create(Client, model);
            author.Update(model);

            var self = PubsubWhisperUser.Create(Client, model.Recipient);
            self.Update(model.Recipient);

            Author = author;
            Self = self;
            Content = model.Body;
            ThreadId = model.ThreadId;
            Timestamp = model.SentTimestamp;
        }
    }
}
