using System;

namespace Orchard.Logging {
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel {
        /// <summary>
        /// 调试
        /// </summary>
        Debug,
        /// <summary>
        /// 信息
        /// </summary>
        Information,
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 致命
        /// </summary>
        Fatal
    }

    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger {
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <returns>true：启用；false：不启用</returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="exception">错误信息</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        void Log(LogLevel level, Exception exception, string format, params object[] args);
    }
}
