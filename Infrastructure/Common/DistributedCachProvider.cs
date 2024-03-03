using Application.Common;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Common
{
    public class DistributedCachProvider : IDistributedCachProvider
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCachProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CachAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), cancellationToken);
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cachedValue))
            {
                return null;
            }
            T model = JsonConvert.DeserializeObject<T>(cachedValue)!;
            return model;
        }

        public async Task InvalidateAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
