using System;

namespace Orchard.Caching {
    /// <summary>
    /// 缓存接口
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TResult">值</typeparam>
    public interface ICache<TKey, TResult> {
        TResult Get(TKey key, Func<AcquireContext<TKey>, TResult> acquire);
    }
}
