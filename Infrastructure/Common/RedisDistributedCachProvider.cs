using Application.Common;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Common
{
    public class RedisDistributedCachProvider : IDistributedCacheProvider
    {
        private readonly IDistributedCache _distributedCache;

        public RedisDistributedCachProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var serializedValue = JsonConvert.SerializeObject(value, serializerSettings);
                await _distributedCache.SetStringAsync(key, serializedValue, cancellationToken);
            }
            catch (Exception)
            {
            }
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
                if (string.IsNullOrEmpty(cachedValue))
                {
                    return null;
                }
                T model = JsonConvert.DeserializeObject<T>(cachedValue)!;
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task InvalidateAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
