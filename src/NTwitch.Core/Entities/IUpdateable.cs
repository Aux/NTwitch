using System.Threading.Tasks;

namespace NTwitch
{
    public interface IUpdateable
    {
        /// <summary> Updates this object's properties with its current state. </summary>
        Task UpdateAsync(RequestOptions options = null);
    }
}
