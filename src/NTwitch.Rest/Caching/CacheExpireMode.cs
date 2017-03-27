namespace NTwitch.Rest
{
    public enum CacheExpireMode
    {
        /// <summary> Do not expire cached enities. </summary>
        None,
        /// <summary> Expire cached entities when cached total is higher than the specified amount. </summary>
        Limit
    }
}
