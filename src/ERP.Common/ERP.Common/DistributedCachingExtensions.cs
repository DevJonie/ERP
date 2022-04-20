using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ERP.Common;
public static class DistributedCachingExtensions
{
    public async static Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
        where T : notnull
    {
        var objectToCache = JsonSerializer.SerializeToUtf8Bytes(value);

        await distributedCache.SetAsync(key, objectToCache, options, token);
    }

    public async static Task<T?> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default)
        where T : class
    {
        var result = await distributedCache.GetAsync(key, token);
        if (result == null) return null;

        var jsonToDeserialize = System.Text.Encoding.UTF8.GetString(result);
        if (jsonToDeserialize is null) return null;

        return JsonSerializer.Deserialize<T>(jsonToDeserialize);
    }

}
