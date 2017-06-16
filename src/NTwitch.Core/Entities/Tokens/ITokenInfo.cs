namespace NTwitch
{
    public interface ITokenInfo
    {
        bool IsValid { get; }
        string Token { get; }
        string Username { get; }
        string ClientId { get; }
        ulong UserId { get; }
        IAuthorization Authorization { get; }
    }
}
