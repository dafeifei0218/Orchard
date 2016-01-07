using System;

namespace Orchard.Logging {
    /// <summary>
    /// 空日志
    /// </summary>
    public class NullLogger : ILogger {
        private static readonly ILogger _instance = new NullLogger();

        /// <summary>
        /// 静态构造函数
        /// </summary>
        public static ILogger Instance {
            get { return _instance; }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level) {
            return false;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public void Log(LogLevel level, Exception exception, string format, params object[] args) {
        }
    }
}