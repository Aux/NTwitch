using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestSimpleCommunity : RestEntity<string>
    {
        public string Name { get; private set; }
        public string AvatarUrl { get; private set; }

        internal RestSimpleCommunity(BaseRestClient client, string id)
            : base(client, id) { }
        
        internal static RestSimpleCommunity Create(BaseRestClient client, Model model)
        {
            var entity = new RestSimpleCommunity(client, model.Id);
            entity.Update(model);
            return entity;
        }

        internal virtual void Update(Model model)
        {
            Name = model.Name;
            AvatarUrl = model.AvatarImageUrl;
        }

        // Communities
        /// <summary> Get the authorized user's permissions for this community. </summary>
        public Task<RestCommunityPermissions> GetPermissionsAsync()
            => CommunityHelper.GetPermissionsAsync(this);
        /// <summary> Change properties for this community. </summary>
        public Task ModifyAsync(Action<ModifyCommunityParams> properties)
            => CommunityHelper.ModifyAsync(this, properties);
        /// <summary> Upload a new avatar image for this community. </summary>
        public Task SetAvatarAsync(string avatarPath)
            => CommunityHelper.SetAvatarAsync(this, avatarPath);
        /// <summary> Upload a new avatar image for this community. </summary>
        public Task SetAvatarAsync(Stream avatarStream)
            => CommunityHelper.SetAvatarAsync(this, avatarStream);
        /// <summary> Remove the avatar image on this community. </summary>
        public Task RemoveAvatarAsync()
            => CommunityHelper.RemoveAvatarAsync(this);
        /// <summary> Upload a new cover image for this community. </summary>
        public Task SetCoverAsync(string coverPath)
            => CommunityHelper.SetCoverAsync(this, coverPath);
        /// <summary> Upload a new cover image for this community. </summary>
        public Task SetCoverAsync(Stream coverStream)
            => CommunityHelper.SetCoverAsync(this, coverStream);
        /// <summary> Remove the cover image on this community. </summary>
        public Task RemoveCoverAsync()
            => CommunityHelper.RemoveCoverAsync(this);

        // Users
        /// <summary> Get users with moderator permissions in this community. </summary>
        public Task<IReadOnlyCollection<RestUser>> GetModeratorsAsync()
            => CommunityHelper.GetModeratorsAsync(this);
        /// <summary> Add a new moderator to this community. </summary>
        public Task AddModeratorAsync(ulong userId)
            => CommunityHelper.AddModeratorAsync(this, userId);
        /// <summary> Remove an existing moderator from this community. </summary>
        public Task RemoveModeratorAsync(ulong userId)
            => CommunityHelper.RemoveModeratorAsync(this, userId);
        /// <summary> Get users banned from this community. </summary>
        public Task<IReadOnlyCollection<RestBannedUser>> GetBannedUsersAsync(uint limit = 10)
            => CommunityHelper.GetBannedUsersAsync(this, limit);
        /// <summary> Add a new ban to this community. </summary>
        public Task AddBanAsync(ulong userId)
            => CommunityHelper.AddBanAsync(this, userId);
        /// <summary> Remove an existing ban from this community. </summary>
        public Task RemoveBanAsync(ulong userId)
            => CommunityHelper.RemoveBanAsync(this, userId);
        /// <summary> Get users timed out of this community. </summary>
        public Task<IReadOnlyCollection<RestBannedUser>> GetTimeoutsAsync(uint limit = 10)
            => CommunityHelper.GetTimeoutsAsync(this, limit);
        /// <summary> Add a new timeout to this community. </summary>
        public Task AddTimeoutAsync(ulong userId, uint duration, string reason)
            => CommunityHelper.AddTimeoutAsync(this, userId, duration, reason);
        /// <summary> Remove an existing timeout from this community. </summary>
        public Task RemoveTimeoutAsync(ulong userId)
            => CommunityHelper.RemoveTimeoutAsync(this, userId);

        // Channels
        /// <summary> Report a channel for violating the rules of this community. </summary>
        public Task ReportChannelAsync(ulong channelId)
            => CommunityHelper.ReportChannelAsync(this, channelId);
    }
}
