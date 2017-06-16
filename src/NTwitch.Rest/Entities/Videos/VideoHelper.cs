using System;
using System.Threading.Tasks;

namespace NTwitch.Rest
{
    internal static class VideoHelper
    {
        public static async Task ModifyAsync(RestVideo video, Action<ModifyVideoParams> changes, RequestOptions options)
        {
            var modify = new ModifyVideoParams();
            changes.Invoke(modify);

            var model = await video.Client.ApiClient.ModifyVideoAsync(video.Id, modify, options);
            video.Update(model);
        }

        public static async Task DeleteAsync(RestVideo video, RequestOptions options)
        {
            await video.Client.ApiClient.DeleteVideoAsync(video.Id, options).ConfigureAwait(false);
        }

        public static Task CreateAsync(string title, Action<CreateVideoParams> changes, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task UploadAsync(RestVideo video, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public static Task CompleteUploadAsync(RestVideo video, RequestOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
