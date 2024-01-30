namespace LetsTravelCoolPlaces.Services.Classes;

public static class CacheService
{
    public static async Task<T?> GetAsync<T>(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default)
    {
        byte[]? utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(continueOnCapturedContext: false);
        if (utf8Bytes != null && utf8Bytes!.Length != 0)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(utf8Bytes);
        }

        return default;
    }

    public static async Task RemoveAsync(this IDistributedCache distributedCache, string cacheKey, CancellationToken token = default)
    {
        await distributedCache.RemoveAsync(cacheKey, token).ConfigureAwait(continueOnCapturedContext: false);
    }

    public static async Task SetAsync<T>(this IDistributedCache distributedCache, string cacheKey, T obj, int cacheExpirationInMinutes = 30, CancellationToken token = default)
    {
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(cacheExpirationInMinutes));
        byte[] utf8Bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj);
        await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(continueOnCapturedContext: false);
    }
}