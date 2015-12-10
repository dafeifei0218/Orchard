using System;

namespace Orchard.Logging {
    /// <summary>
    /// 日志工厂
    /// </summary>
    public interface ILoggerFactory {
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        ILogger CreateLogger(Type type);
    }
}