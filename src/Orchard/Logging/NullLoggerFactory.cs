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
        /// <returns>返回空日志对象</returns>
        public ILogger CreateLogger(Type type) {
            return NullLogger.Instance;
        }
    }
}