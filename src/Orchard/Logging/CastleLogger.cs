using System;

namespace Orchard.Logging {
    /// <summary>
    /// Castle日志类
    /// </summary>
    public class CastleLogger : ILogger {
        //Castle日志
        private readonly Castle.Core.Logging.ILogger _castleLogger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="castleLogger">Castle日志</param>
        public CastleLogger(Castle.Core.Logging.ILogger castleLogger) {
            _castleLogger = castleLogger;
            
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public void Error(Exception exception, string format, params object[] args) {
            _castleLogger.ErrorFormat(exception, format, args);
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level) {
            switch(level) {
                case LogLevel.Debug:
                    return _castleLogger.IsDebugEnabled;
                case LogLevel.Information:
                    return _castleLogger.IsInfoEnabled;
                case LogLevel.Warning:
                    return _castleLogger.IsWarnEnabled;
                case LogLevel.Error:
                    return _castleLogger.IsErrorEnabled;
                case LogLevel.Fatal:
                    return _castleLogger.IsFatalEnabled;
            }
            return false;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        /// <param name="format">格式</param>
        /// <param name="args">对象集合</param>
        public void Log(LogLevel level, Exception exception, string format, params object[] args) {
            if (args == null) {
                switch (level) {
                    case LogLevel.Debug:
                        _castleLogger.Debug(format, exception);
                        break;
                    case LogLevel.Information:
                        _castleLogger.Info(format, exception);
                        break;
                    case LogLevel.Warning:
                        _castleLogger.Warn(format, exception);
                        break;
                    case LogLevel.Error:
                        _castleLogger.Error(format, exception);
                        break;
                    case LogLevel.Fatal:
                        _castleLogger.Fatal(format, exception);
                        break;
                }
            }
            else {
                switch (level) {
                    case LogLevel.Debug:
                        _castleLogger.DebugFormat(exception, format, args);
                        break;
                    case LogLevel.Information:
                        _castleLogger.InfoFormat(exception, format, args);
                        break;
                    case LogLevel.Warning:
                        _castleLogger.WarnFormat(exception, format, args);
                        break;
                    case LogLevel.Error:
                        _castleLogger.ErrorFormat(exception, format, args);
                        break;
                    case LogLevel.Fatal:
                        _castleLogger.FatalFormat(exception, format, args);
                        break;
                }
            }
        }
    }
}
