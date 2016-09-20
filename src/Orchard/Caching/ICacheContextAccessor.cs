namespace Orchard.Caching {
    /// <summary>
    /// 缓存上下文访问器接口
    /// </summary>
    public interface ICacheContextAccessor {
        /// <summary>
        /// 获取或设置缓存上下文
        /// </summary>
        IAcquireContext Current { get; set; }
    }
}