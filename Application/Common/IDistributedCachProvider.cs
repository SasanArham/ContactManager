namespace Application.Common
{
    public interface IDistributedCachProvider
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
        Task CachAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class;
        Task InvalidateAsync(string key, CancellationToken cancellationToken = default);
    }
}
