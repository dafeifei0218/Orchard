namespace Orchard.Caching {
    /// <summary>
    /// 缓存上下文访问器
    /// </summary>
    public interface ICacheContextAccessor {
        /// <summary>
        /// 
        /// </summary>
        IAcquireContext Current { get; set; }
    }
}