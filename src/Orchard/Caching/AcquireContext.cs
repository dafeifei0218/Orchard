using System;

namespace Orchard.Caching {
    /// <summary>
    /// 获取上下文
    /// </summary>
    public interface IAcquireContext {
        /// <summary>
        /// 监控，获取挥发令牌
        /// </summary>
        Action<IVolatileToken> Monitor { get; }
    }

    /// <summary>
    /// 获取上下文接口
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    public class AcquireContext<TKey> : IAcquireContext {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="monitor">监控，获取挥发令牌</param>
        public AcquireContext(TKey key, Action<IVolatileToken> monitor) {
            Key = key;
            Monitor = monitor;
        }

        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; private set; }
        /// <summary>
        /// 监控，获取挥发令牌
        /// </summary>
        public Action<IVolatileToken> Monitor { get; private set; }
    }

    /// <summary>
    /// Simple implementation of "IAcquireContext" given a lamdba
    /// 简单实现IAcquireContext获取上下文
    /// </summary>
    public class SimpleAcquireContext : IAcquireContext {
        private readonly Action<IVolatileToken> _monitor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="monitor">监控，获取挥发令牌</param>
        public SimpleAcquireContext(Action<IVolatileToken> monitor) {
            _monitor = monitor;
        }

        /// <summary>
        /// 监控，获取挥发令牌
        /// </summary>
        public Action<IVolatileToken> Monitor {
            get { return _monitor; }
        }
    }
}
