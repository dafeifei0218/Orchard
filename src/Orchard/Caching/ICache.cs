using System;

namespace Orchard.Caching {
    /// <summary>
    /// 缓存接口
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TResult">缓存结果</typeparam>
    public interface ICache<TKey, TResult> {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="acquire">获取上下文的委托</param>
        /// <returns></returns>
        TResult Get(TKey key, Func<AcquireContext<TKey>, TResult> acquire);
    }
}
