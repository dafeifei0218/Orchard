namespace Orchard.Caching {
    /// <summary>
    /// 挥发令牌接口
    /// </summary>
    public interface IVolatileToken {
        /// <summary>
        /// 是否是当前的对象，false：缓存失效，ture：缓存有效
        /// </summary>
        bool IsCurrent { get; }
    }
}