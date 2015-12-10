using System;

namespace Orchard.Logging {
    /// <summary>
    /// 空日志工厂
    /// </summary>
    class NullLoggerFactory : ILoggerFactory {
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type) {
            return NullLogger.Instance;
        }
    }
}