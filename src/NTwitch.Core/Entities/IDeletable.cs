using System.Threading.Tasks;

namespace NTwitch
{
    public interface IDeletable
    {
        /// <summary> Deletes this object and all its children. </summary>
        Task DeleteAsync(RequestOptions options = null);
    }
}
