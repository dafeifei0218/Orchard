using System;
using System.Collections.Generic;
using System.Linq;

namespace Orchard.Caching {
    /// <summary>
    /// 
    /// </summary>
    public class DefaultCacheContextAccessor : ICacheContextAccessor {
        /// <summary>
        /// 
        /// </summary>
        [ThreadStatic]
        private static IAcquireContext _threadInstance;

        /// <summary>
        /// 
        /// </summary>
        public static IAcquireContext ThreadInstance {
            get { return _threadInstance; }
            set { _threadInstance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IAcquireContext Current {
            get { return ThreadInstance; }
            set { ThreadInstance = value; }
        }
    }
}