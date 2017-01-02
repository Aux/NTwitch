using System.Runtime.CompilerServices;

namespace NTwitch
{
    public interface IEntity
    {
        ITwitchClient Client { get; }
        ulong Id { get; }
    }
}
