using System;

namespace Orchard.Caching {
    /// <summary>
    /// 缓存接口
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TResult">缓存结果</typeparam>
    public interface ICache<TKey, TResult> {
        /// <summary>
        /// 获取关村
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        TResult Get(TKey key, Func<AcquireContext<TKey>, TResult> acquire);
    }
}
