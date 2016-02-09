using System;

namespace Orchard.Caching {
    /// <summary>
    /// 
    /// </summary>
    public interface IAcquireContext {
        /// <summary>
        /// 监控
        /// </summary>
        Action<IVolatileToken> Monitor { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    public class AcquireContext<TKey> : IAcquireContext {
        public AcquireContext(TKey key, Action<IVolatileToken> monitor) {
            Key = key;
            Monitor = monitor;
        }

        public TKey Key { get; private set; }
        public Action<IVolatileToken> Monitor { get; private set; }
    }

    /// <summary>
    /// Simple implementation of "IAcquireContext" given a lamdba
    /// 简单实现上下文
    /// </summary>
    public class SimpleAcquireContext : IAcquireContext {
        private readonly Action<IVolatileToken> _monitor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="monitor"></param>
        public SimpleAcquireContext(Action<IVolatileToken> monitor) {
            _monitor = monitor;
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<IVolatileToken> Monitor {
            get { return _monitor; }
        }
    }
}
