namespace Application.Common
{
    public interface IDistributedCacheProvider
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
        Task CacheAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class;
        Task InvalidateAsync(string key, CancellationToken cancellationToken = default);
        Task CacheInSetAsync<T>(string key, T value, int ID) where T : class;
        Task<long> InvalidateFromSetByIDAsync(string key, int score);
        Task<long> InvalidateAllInSetAsync(string key);
        Task<List<T>> GetItemsFromSetAsync<T>(string key, int skip = 0, int take = int.MaxValue, bool ascending = false) where T : class;
        Task<long> SetLenghtAsync(string key);
        Task<T?> PopFromSetAsync<T>(string key) where T : class;
    }
}
