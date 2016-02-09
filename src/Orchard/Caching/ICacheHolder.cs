using System;

namespace Orchard.Caching {
    /// <summary>
    /// 维护ICache接口集合（生命周期：租户单例）
    /// </summary>
    public interface ICacheHolder : ISingletonDependency {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TResult">结果</typeparam>
        /// <param name="component">组件</param>
        /// <returns>缓存</returns>
        ICache<TKey, TResult> GetCache<TKey, TResult>(Type component);
    }
}
