using System.Threading.Tasks;

namespace NTwitch
{
    public interface IUpdateable
    {
        Task UpdateAsync();
    }
}
