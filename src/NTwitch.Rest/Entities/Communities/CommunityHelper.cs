using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class CommunityHelper
    {
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

            byte[] imageBytes;
            byte[] buffer = new byte[16 * 1024];
            using (var memory = new MemoryStream())
            {
                int read;
                while ((read = avatarStream.Read(buffer, 0, buffer.Length)) > 0)
                    memory.Write(buffer, 0, read);
                imageBytes = memory.ToArray();
            }
            
            var imageString = Convert.ToBase64String(imageBytes);

            await community.Client.RestClient.SetAvatarAsync(imageString).ConfigureAwait(false);

            if (community is RestCommunity c)
                await c.UpdateAsync().ConfigureAwait(false);
        }
    }
}
