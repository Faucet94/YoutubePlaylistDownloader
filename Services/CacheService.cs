using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

public class CacheService
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
    {
        if (!_cache.TryGetValue(key, out T result))
        {
            result = await factory();
            _cache.Set(key, result, _defaultExpiration);
        }
        return result;
    }
} 