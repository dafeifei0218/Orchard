using System;
using System.Collections.Generic;
using System.Linq;

namespace Orchard.Caching {
    /// <summary>
    /// 默认缓存上下文访问器
    /// </summary>
    public class DefaultCacheContextAccessor : ICacheContextAccessor {
        /// <summary>
        /// 线程实例
        /// </summary>
        [ThreadStatic]
        private static IAcquireContext _threadInstance;

        /// <summary>
        /// 线程实例
        /// </summary>
        public static IAcquireContext ThreadInstance {
            get { return _threadInstance; }
            set { _threadInstance = value; }
        }

        /// <summary>
        /// 获取或设置当前线程实例
        /// </summary>
        public IAcquireContext Current {
            get { return ThreadInstance; }
            set { ThreadInstance = value; }
        }
    }
}