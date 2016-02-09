using System;

namespace Orchard.Caching {
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheManager {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        TResult Get<TKey, TResult>(TKey key, Func<AcquireContext<TKey>, TResult> acquire);

        /// <summary>
        /// 从缓存持有者中获取一个缓存。
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        ICache<TKey, TResult> GetCache<TKey, TResult>();
    }

    public static class CacheManagerExtensions {
        public static TResult Get<TKey, TResult>(this ICacheManager cacheManager, TKey key, bool preventConcurrentCalls, Func<AcquireContext<TKey>, TResult> acquire) {
            if (preventConcurrentCalls) {
                lock(key) {
                    return cacheManager.Get(key, acquire);
                }
            }
            else {
                return cacheManager.Get(key, acquire);
            }
        }
    }
}
