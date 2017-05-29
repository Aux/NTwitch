using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Model = NTwitch.Rest.API.Community;

namespace NTwitch.Rest
{
    public class RestSimpleCommunity : RestEntity<string>, IEqualityComparer<RestSimpleCommunity>
    {
        /// <summary> The name of this community </summary>
        public string Name { get; private set; }
        /// <summary> The url to the avatar image of this community </summary>
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

        public bool Equals(RestSimpleCommunity x, RestSimpleCommunity y)
            => x.Id == y.Id;
        public int GetHashCode(RestSimpleCommunity obj)
            => obj.GetHashCode();

        // Communities
        /// <summary> Get the authorized user's permissions for this community. </summary>
        public Task<RestCommunityPermissions> GetPermissionsAsync(ulong userId)
            => CommunityHelper.GetPermissionsAsync(this, userId);
        /// <summary> Change properties for this community. </summary>
        public Task ModifyAsync(ulong userId, Action<ModifyCommunityParams> properties)
            => CommunityHelper.ModifyAsync(this, userId, properties);
        /// <summary> Upload a new avatar image for this community. </summary>
        public Task SetAvatarAsync(ulong userId, string avatarPath)
            => CommunityHelper.SetAvatarAsync(this, userId, avatarPath);
        /// <summary> Upload a new avatar image for this community. </summary>
        public Task SetAvatarAsync(ulong userId, Stream avatarStream)
            => CommunityHelper.SetAvatarAsync(this, userId, avatarStream);
        /// <summary> Remove the avatar image on this community. </summary>
        public Task RemoveAvatarAsync(ulong userId)
            => CommunityHelper.RemoveAvatarAsync(this, userId);
        /// <summary> Upload a new cover image for this community. </summary>
        public Task SetCoverAsync(ulong userId, string coverPath)
            => CommunityHelper.SetCoverAsync(this, userId, coverPath);
        /// <summary> Upload a new cover image for this community. </summary>
        public Task SetCoverAsync(ulong userId, Stream coverStream)
            => CommunityHelper.SetCoverAsync(this, userId, coverStream);
        /// <summary> Remove the cover image on this community. </summary>
        public Task RemoveCoverAsync(ulong userId)
            => CommunityHelper.RemoveCoverAsync(this, userId);

        // Users
        /// <summary> Get users with moderator permissions in this community. </summary>
        public Task<IReadOnlyCollection<RestUser>> GetModeratorsAsync(ulong userId)
            => CommunityHelper.GetModeratorsAsync(this, userId);
        /// <summary> Add a new moderator to this community. </summary>
        public Task AddModeratorAsync(ulong userId, ulong victimId)
            => CommunityHelper.AddModeratorAsync(this, userId, victimId);
        /// <summary> Remove an existing moderator from this community. </summary>
        public Task RemoveModeratorAsync(ulong userId, ulong victimId)
            => CommunityHelper.RemoveModeratorAsync(this, userId, victimId);
        /// <summary> Get users banned from this community. </summary>
        public Task<IReadOnlyCollection<RestBannedUser>> GetBansAsync(ulong userId, uint limit = 10)
            => CommunityHelper.GetBansAsync(this, userId, limit);
        /// <summary> Add a new ban to this community. </summary>
        public Task AddBanAsync(ulong userId, ulong victimId)
            => CommunityHelper.AddBanAsync(this, userId, victimId);
        /// <summary> Remove an existing ban from this community. </summary>
        public Task RemoveBanAsync(ulong userId, ulong victimId)
            => CommunityHelper.RemoveBanAsync(this, userId, victimId);
        /// <summary> Get users timed out of this community. </summary>
        public Task<IReadOnlyCollection<RestBannedUser>> GetTimeoutsAsync(ulong userId, uint limit = 10)
            => CommunityHelper.GetTimeoutsAsync(this, userId, limit);
        /// <summary> Add a new timeout to this community. </summary>
        public Task AddTimeoutAsync(ulong userId, ulong victimId, uint duration, string reason)
            => CommunityHelper.AddTimeoutAsync(this, userId, victimId, duration, reason);
        /// <summary> Remove an existing timeout from this community. </summary>
        public Task RemoveTimeoutAsync(ulong userId, ulong victimId)
            => CommunityHelper.RemoveTimeoutAsync(this, userId, victimId);

        // Channels
        /// <summary> Report a channel for violating the rules of this community. </summary>
        public Task ReportChannelAsync(ulong userId, ulong channelId)
            => CommunityHelper.ReportChannelAsync(this, userId, channelId);
    }
}
