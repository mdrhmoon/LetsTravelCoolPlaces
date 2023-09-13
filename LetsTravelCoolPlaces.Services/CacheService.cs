namespace LetsTravelCoolPlaces.Services;

public static class CacheService
{
    public static async Task<T?> GetAsync<T>(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default(CancellationToken))
    {
        byte[]? utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(continueOnCapturedContext: false);
        if (utf8Bytes != null)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(utf8Bytes);
        }

        return default(T);
    }

    public static async Task RemoveAsync(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default(CancellationToken))
    {
        await distributedCache.RemoveAsync(cacheKey, token).ConfigureAwait(continueOnCapturedContext: false);
    }

    public static async Task SetAsync<T>(this IDistributedCache distributedCache, string cacheKey, T obj, int cacheExpirationInMinutes = 30, CancellationToken token = default(CancellationToken))
    {
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(cacheExpirationInMinutes));
        byte[] utf8Bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj);
        await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(continueOnCapturedContext: false);
    }
}