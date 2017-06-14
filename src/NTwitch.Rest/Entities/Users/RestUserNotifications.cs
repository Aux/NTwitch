namespace NTwitch.Rest
{
    public class RestUserNotifications
    {
        /// <summary> True if this user receives push notifications </summary>
        public bool IsPushEnabled { get; private set; }
        /// <summary> True if this user receives email notifications </summary>
        public bool IsEmailEnabled { get; private set; }

        internal RestUserNotifications() { }

        internal static RestUserNotifications Create(UserNotifications model)
        {
            var entity = new RestUserNotifications();
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(UserNotifications model)
        {
            IsPushEnabled = model.IsPushEnabled;
            IsEmailEnabled = model.IsEmailEnabled;
        }
    }
}
