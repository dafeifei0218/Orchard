using System;

namespace Orchard.Logging {
    /// <summary>
    /// 日志扩展类
    /// </summary>
    public static class LoggingExtensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="message">日志消息</param>
        public static void Debug(this ILogger logger, string message) {
            FilteredLog(logger, LogLevel.Debug, null, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="message">日志消息</param>
        public static void Information(this ILogger logger, string message) {
            FilteredLog(logger, LogLevel.Information, null, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="message">日志消息</param>
        public static void Warning(this ILogger logger, string message) {
            FilteredLog(logger, LogLevel.Warning, null, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="message">日志消息</param>
        public static void Error(this ILogger logger, string message) {
            FilteredLog(logger, LogLevel.Error, null, message, null);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="message">日志消息</param>
        public static void Fatal(this ILogger logger, string message) {
            FilteredLog(logger, LogLevel.Fatal, null, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        public static void Debug(this ILogger logger, Exception exception, string message) {
            FilteredLog(logger, LogLevel.Debug, exception, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        public static void Information(this ILogger logger, Exception exception, string message) {
            FilteredLog(logger, LogLevel.Information, exception, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        public static void Warning(this ILogger logger, Exception exception, string message) {
            FilteredLog(logger, LogLevel.Warning, exception, message, null);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        public static void Error(this ILogger logger, Exception exception, string message) {
            FilteredLog(logger, LogLevel.Error, exception, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="message">日志消息</param>
        public static void Fatal(this ILogger logger, Exception exception, string message) {
            FilteredLog(logger, LogLevel.Fatal, exception, message, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Debug(this ILogger logger, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Debug, null, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Information(this ILogger logger, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Information, null, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Warning(this ILogger logger, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Warning, null, format, args);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Error(this ILogger logger, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Error, null, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Fatal(this ILogger logger, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Fatal, null, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Debug(this ILogger logger, Exception exception, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Debug, exception, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Information(this ILogger logger, Exception exception, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Information, exception, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Warning(this ILogger logger, Exception exception, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Warning, exception, format, args);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Error(this ILogger logger, Exception exception, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Error, exception, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public static void Fatal(this ILogger logger, Exception exception, string format, params object[] args) {
            FilteredLog(logger, LogLevel.Fatal, exception, format, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="objects">对象集合</param>
        private static void FilteredLog(ILogger logger, LogLevel level, Exception exception, string format, object[] objects) {
            if (logger.IsEnabled(level)) {
                logger.Log(level, exception, format, objects);
            }
        }
    }
}