namespace Application.Common
{
    public interface IDistributedCacheProvider
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
        Task CacheAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class;
        Task InvalidateAsync(string key, CancellationToken cancellationToken = default);
    }
}
