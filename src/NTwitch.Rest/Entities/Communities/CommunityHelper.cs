using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class CommunityHelper
    {
        public static string GetBase64String(Stream stream)
        {
            byte[] bytes;
            byte[] buffer = new byte[16 * 1024];
            using (var memory = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    memory.Write(buffer, 0, read);
                bytes = memory.ToArray();
            }

            return Convert.ToBase64String(bytes);
        }

        public static Task ReportChannelAsync(RestUserCommunity community, ulong channelId)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new InvalidOperationException("No valid token specified for this user.");

            return community.Client.RestClient.CommunityReportInternalAsync(info.Token, community.Id, channelId);
        }

        #region Communities

        public static async Task<RestCommunityPermissions> GetPermissionsAsync(RestUserCommunity community)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new InvalidOperationException("No valid token specified for this user.");
            
            var model = await community.Client.RestClient.GetCommunityPermissionsInternalAsync(info.Token, community.Id);
            var entity = new RestCommunityPermissions();
            entity.Update(model);
            return entity;
        }

        public static Task ModifyAsync(RestUserCommunity community, Action<ModifyCommunityParams> options)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var changes = new ModifyCommunityParams();
            options.Invoke(changes);

            return community.Client.RestClient.ModifyCommunityInternalAsync(info.Token, community.Id, changes);
        }

        public static Task SetAvatarAsync(RestUserCommunity community, string avatarPath)
        {
            var avatarStream = File.Open(avatarPath, FileMode.Open);
            return SetAvatarAsync(community, avatarStream);
        }

        public static async Task SetAvatarAsync(RestUserCommunity community, Stream avatarStream)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var imageString = GetBase64String(avatarStream);
            await community.Client.RestClient.SetAvatarInternalAsync(info.Token, community.Id, imageString).ConfigureAwait(false);

            //if (community is RestCommunity c)
            //    await c.UpdateAsync().ConfigureAwait(false);
        }

        public static Task RemoveAvatarAsync(RestUserCommunity community)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            return community.Client.RestClient.RemoveAvatarInternalAsync(info.Token, community.Id);
        }

        public static Task SetCoverAsync(RestUserCommunity community, string coverPath)
        {
            var coverStream = File.Open(coverPath, FileMode.Open);
            return SetCoverAsync(community, coverStream);
        }

        public static async Task SetCoverAsync(RestUserCommunity community, Stream coverStream)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var imageString = GetBase64String(coverStream);
            await community.Client.RestClient.SetCoverInternalAsync(info.Token, community.Id, imageString).ConfigureAwait(false);

            //if (community is RestCommunity c)
            //    await c.UpdateAsync().ConfigureAwait(false);
        }

        public static Task RemoveCoverAsync(RestUserCommunity community)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            return community.Client.RestClient.RemoveCoverInternalAsync(info.Token, community.Id);
        }

        #endregion
        #region Users

        public static async Task<IReadOnlyCollection<RestUser>> GetModeratorsAsync(RestUserCommunity community)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var model = await community.Client.RestClient.GetCommunityModeratorsInternalAsync(info.Token, community.Id);

            var entity = model.Moderators.Select(x =>
            {
                var user = new RestUser(community.Client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        public static Task AddModeratorAsync(RestUserCommunity community, ulong victimId)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            return community.Client.RestClient.AddCommunityModeratorInternalAsync(info.Token, community.Id, victimId);
        }

        public static Task RemoveModeratorAsync(RestUserCommunity community, ulong victimId)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            return community.Client.RestClient.RemoveCommunityModeratorInternalAsync(info.Token, community.Id, victimId);
        }

        public static async Task<IReadOnlyCollection<RestBannedUser>> GetBansAsync(RestUserCommunity community, uint limit)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_edit");
            if (!info.Authorization.Scopes.Contains("communities_moderate"))
                throw new MissingScopeException("communities_moderate");

            var model = await community.Client.RestClient.GetCommunityBansInternalAsync(info.Token, community.Id, limit);

            var entity = model.Bans.Select(x =>
            {
                var user = new RestBannedUser(community.Client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        public static Task AddBanAsync(RestUserCommunity community, ulong victimId)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_moderate");
            if (!info.Authorization.Scopes.Contains("communities_moderate"))
                throw new MissingScopeException("communities_moderate");

            return community.Client.RestClient.AddCommunityBanInternalAsync(info.Token, community.Id, victimId);
        }

        public static Task RemoveBanAsync(RestUserCommunity community, ulong victimId)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_moderate");
            if (!info.Authorization.Scopes.Contains("communities_moderate"))
                throw new MissingScopeException("communities_moderate");

            return community.Client.RestClient.RemoveCommunityBanInternalAsync(info.Token, community.Id, victimId);
        }

        public static async Task<IReadOnlyCollection<RestBannedUser>> GetTimeoutsAsync(RestUserCommunity community, uint limit)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_moderate");
            if (!info.Authorization.Scopes.Contains("communities_moderate"))
                throw new MissingScopeException("communities_moderate");

            var model = await community.Client.RestClient.GetCommunityTimeoutsInternalAsync(info.Token, community.Id, limit);

            var entity = model.Timeouts.Select(x =>
            {
                var user = new RestBannedUser(community.Client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        public static Task AddTimeoutAsync(RestUserCommunity community, ulong victimId, uint duration, string reason)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_moderate");
            if (!info.Authorization.Scopes.Contains("communities_moderate"))
                throw new MissingScopeException("communities_moderate");

            return community.Client.RestClient.AddCommunityTimeoutInternalAsync(info.Token, community.Id, victimId);
        }

        public static Task RemoveTimeoutAsync(RestUserCommunity community, ulong victimId)
        {
            if (!TokenHelper.TryGetToken(community.Client, community.CurrentUser.Id, out RestTokenInfo info))
                throw new MissingScopeException("communities_moderate");
            if (!info.Authorization.Scopes.Contains("communities_moderate"))
                throw new MissingScopeException("communities_moderate");

            return community.Client.RestClient.RemoveCommunityTimeoutInternalAsync(info.Token, community.Id, victimId);
        }

        #endregion
        #region User Communities

        public static async Task<RestUserCommunity> GetUserCommunityAsync(RestCommunity community, ulong userId)
        {
            var user = await community.Client.GetSelfUserAsync(userId);
            var usercommunity = await GetUserCommunityAsync(community, user);
            return usercommunity;
        }

        public static async Task<RestUserCommunity> GetUserCommunityAsync(IUser user, string communityId, bool isname)
        {
            var client = user.Client as TwitchRestClient;
            var community = await client.GetCommunityAsync(communityId, isname);
            var usercommunity = await GetUserCommunityAsync(community, user);
            return usercommunity;
        }

        public static Task<RestUserCommunity> GetUserCommunityAsync(RestCommunity community, IUser user)
        {
            var usercommunity = RestUserCommunity.Create(community, user);
            return Task.FromResult(usercommunity);
        }

        #endregion
    }
}
