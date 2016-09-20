using System;

namespace Orchard.Logging {
    /// <summary>
    /// 日志工厂接口
    /// </summary>
    public interface ILoggerFactory {
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>日志接口</returns>
        ILogger CreateLogger(Type type);
    }
}