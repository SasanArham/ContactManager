using Application.Common;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Common
{
    public class RedisDistributedCachProvider : IDistributedCacheProvider
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisDistributedCachProvider(IDistributedCache distributedCache
            , IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task CacheAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var serializedValue = JsonConvert.SerializeObject(value, serializerSettings);
            await _distributedCache.SetStringAsync(key, serializedValue, cancellationToken);
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

        public async Task CacheInSetAsync<T>(string key, T value, int score) where T : class
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var serializedValue = JsonConvert.SerializeObject(value, serializerSettings);

            var cacheDb = _connectionMultiplexer.GetDatabase();
            await cacheDb.SortedSetAddAsync(key, serializedValue, score);
        }

        public async Task<long> InvalidateFromSetByIDAsync(string key, int score)
        {
            var cacheDb = _connectionMultiplexer.GetDatabase();
            return await cacheDb.SortedSetRemoveRangeByScoreAsync(key, score, score);
        }

        public async Task<long> InvalidateAllInSetAsync(string key)
        {
            var cacheDb = _connectionMultiplexer.GetDatabase();
            return await cacheDb.SortedSetRemoveRangeByRankAsync(key, 0, long.MaxValue);
        }

        public async Task<List<T>> GetItemsFromSetAsync<T>(string key, int skip = 0, int take = int.MaxValue, bool ascending = false) where T : class
        {
            var cacheDb = _connectionMultiplexer.GetDatabase();
            var order = ascending ? Order.Ascending : Order.Descending;
            var cachedItems = await cacheDb.SortedSetRangeByScoreAsync(key, skip: skip, take: take, order: order);
            var deserializedObjects = cachedItems.Select(entry => JsonConvert.DeserializeObject<T>(entry.ToString())).ToList();
            return deserializedObjects;
        }

        public async Task<long> SetLenghtAsync(string key)
        {
            var cacheDb = _connectionMultiplexer.GetDatabase();
            return await cacheDb.SortedSetLengthAsync(key);
        }

        public async Task<T?> PopFromSetAsync<T>(string key) where T : class
        {
            var cacheDb = _connectionMultiplexer.GetDatabase();
            var item = await cacheDb.SortedSetPopAsync(key);
            var serializedItem = item.ToString();
            serializedItem = serializedItem.Substring(0, serializedItem.Length - 4);
            var deserializedObject = JsonConvert.DeserializeObject<T>(serializedItem);
            return deserializedObject;
        }
    }
}
