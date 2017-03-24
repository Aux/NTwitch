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

        #region Communities

        internal static async Task<RestCommunityPermissions> GetPermissionsAsync(RestSimpleCommunity community)
        {
            var model = await community.Client.RestClient.GetCommunityPermissionsAsync(community.Id);
            var entity = new RestCommunityPermissions();
            entity.Update(model);
            return entity;
        }

        public static Task ModifyAsync(RestSimpleCommunity community, Action<ModifyCommunityParams> properties)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            return community.Client.RestClient.ModifyCommunityAsync(community.Id, properties);
        }

        public static Task SetAvatarAsync(RestSimpleCommunity community, string avatarPath)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var avatarStream = File.Open(avatarPath, FileMode.Open);
            return SetAvatarAsync(community, avatarStream);
        }

        public static async Task SetAvatarAsync(RestSimpleCommunity community, Stream avatarStream)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var imageString = GetBase64String(avatarStream);
            await community.Client.RestClient.SetAvatarAsync(community.Id, imageString).ConfigureAwait(false);

            if (community is RestCommunity c)
                await c.UpdateAsync().ConfigureAwait(false);
        }

        public static Task RemoveAvatarAsync(RestSimpleCommunity community)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            return community.Client.RestClient.RemoveAvatarAsync(community.Id);
        }

        public static Task SetCoverAsync(RestSimpleCommunity community, string coverPath)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");
            
            var coverStream = File.Open(coverPath, FileMode.Open);
            return SetCoverAsync(community, coverStream);
        }

        public static async Task SetCoverAsync(RestSimpleCommunity community, Stream coverStream)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var imageString = GetBase64String(coverStream);
            await community.Client.RestClient.SetCoverAsync(community.Id, imageString).ConfigureAwait(false);

            if (community is RestCommunity c)
                await c.UpdateAsync().ConfigureAwait(false);
        }

        public static Task RemoveCoverAsync(RestSimpleCommunity community)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            return community.Client.RestClient.RemoveCoverAsync(community.Id);
        }

        #endregion
        #region Users

        public static async Task<IReadOnlyCollection<RestUser>> GetModeratorsAsync(RestSimpleCommunity community)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var model = await community.Client.RestClient.GetCommunityModeratorsAsync(community.Id);

            var entity = model.Moderators.Select(x =>
            {
                var user = new RestUser(community.Client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestBannedUser>> GetBansAsync(RestSimpleCommunity community, uint limit)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var model = await community.Client.RestClient.GetCommunityBansAsync(community.Id, limit);

            var entity = model.Bans.Select(x =>
            {
                var user = new RestBannedUser(community.Client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }

        internal static async Task<IReadOnlyCollection<RestBannedUser>> GetTimeoutsAsync(RestSimpleCommunity community, uint limit)
        {
            if (!community.Client.Token.Authorization.Scopes.Contains("communities_edit"))
                throw new MissingScopeException("communities_edit");

            var model = await community.Client.RestClient.GetCommunityTimeoutsAsync(community.Id, limit);

            var entity = model.Timeouts.Select(x =>
            {
                var user = new RestBannedUser(community.Client, x.Id);
                user.Update(x);
                return user;
            });
            return entity.ToArray();
        }


        #endregion
    }
}
