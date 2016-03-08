using System;

namespace Orchard.Caching {
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheManager {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TResult">结果</typeparam>
        /// <param name="key">键</param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        TResult Get<TKey, TResult>(TKey key, Func<AcquireContext<TKey>, TResult> acquire);

        /// <summary>
        /// 从缓存持有者中获取一个缓存。
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TResult">结果</typeparam>
        /// <returns></returns>
        ICache<TKey, TResult> GetCache<TKey, TResult>();
    }

    /// <summary>
    /// 缓存管理扩展类
    /// </summary>
    public static class CacheManagerExtensions {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TResult">结果</typeparam>
        /// <param name="cacheManager">缓存管理</param>
        /// <param name="key">键</param>
        /// <param name="preventConcurrentCalls"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
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
