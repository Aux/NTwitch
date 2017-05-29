using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    public class RestUserCommunity : RestCommunity
    {
        /// <summary> The user used to access this community </summary>
        public IUser CurrentUser { get; private set; }

        internal RestUserCommunity(RestCommunity community)
            : base(community.Client, community.Id) { }

        internal static RestUserCommunity Create(RestCommunity community, IUser user)
        {
            var entity = new RestUserCommunity(community);
            entity.Update(community);
            return entity;
        }

        internal void Update(RestUserCommunity community)
        {
            base.Update(community);
        }

        // Community
        public Task<RestCommunityPermissions> GetPermissions()
            => CommunityHelper.GetPermissionsAsync(this);
        public Task ModifyAsync(Action<ModifyCommunityParams> options)
            => CommunityHelper.ModifyAsync(this, options);
        public Task ReportAsync(ulong channelId)
            => CommunityHelper.ReportChannelAsync(this, channelId);

        // Avatars
        public Task SetAvatarAsync(string filePath)
            => CommunityHelper.SetAvatarAsync(this, filePath);
        public Task SetAvatarAsync(Stream fileStream)
            => CommunityHelper.SetAvatarAsync(this, fileStream);
        public Task RemoveAvatarAsync()
            => CommunityHelper.RemoveAvatarAsync(this);

        // Covers
        public Task SetCoverAsync(string filePath)
            => CommunityHelper.SetCoverAsync(this, filePath);
        public Task SetCoverAsync(Stream fileStream)
            => CommunityHelper.SetCoverAsync(this, fileStream);
        public Task RemoveCoverAsync()
            => CommunityHelper.RemoveCoverAsync(this);

        // Moderators
        public Task<IReadOnlyCollection<RestUser>> GetModeratorsAsync()
            => CommunityHelper.GetModeratorsAsync(this);
        public Task AddModeratorAsync(ulong userId)
            => CommunityHelper.AddModeratorAsync(this, userId);
        public Task RemoveModeratorAsync(ulong userId)
            => CommunityHelper.RemoveModeratorAsync(this, userId);

        // Bans
        public Task<IReadOnlyCollection<RestBannedUser>> GetBansAsync(uint amount)
            => CommunityHelper.GetBansAsync(this, amount);
        public Task AddBanAsync(ulong userId)
            => CommunityHelper.AddBanAsync(this, userId);
        public Task RemoveBanAsync(ulong userId)
            => CommunityHelper.RemoveBanAsync(this, userId);

        // Timeouts
        public Task<IReadOnlyCollection<RestBannedUser>> GetTimeoutsAsync(uint amount)
            => CommunityHelper.GetTimeoutsAsync(this, amount);
        public Task AddTimeoutAsync(ulong userId, uint duration, string reason)
            => CommunityHelper.AddTimeoutAsync(this, userId, duration, reason);
        public Task RemoveTimeoutAsync(ulong userId)
            => CommunityHelper.RemoveTimeoutAsync(this, userId);
    }
}
