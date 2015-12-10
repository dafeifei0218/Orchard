using System;
using System.Configuration;
using Castle.Core.Logging;
using log4net;
using log4net.Config;
using Orchard.Environment;

namespace Orchard.Logging {
    /// <summary>
    /// OrchardLog4net日志工厂
    /// </summary>
    public class OrchardLog4netFactory : AbstractLoggerFactory {
        private static bool _isFileWatched = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostEnvironment">主机环境</param>
        public OrchardLog4netFactory(IHostEnvironment hostEnvironment) 
            : this(ConfigurationManager.AppSettings["log4net.Config"], hostEnvironment) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configFilename">配置文件名</param>
        /// <param name="hostEnvironment">主机环境</param>
        public OrchardLog4netFactory(string configFilename, IHostEnvironment hostEnvironment) {
            if (!_isFileWatched && !string.IsNullOrWhiteSpace(configFilename)) {
                // Only monitor configuration file in full trust
                XmlConfigurator.ConfigureAndWatch(GetConfigFile(configFilename));
                _isFileWatched = true;
            }
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="name">日志名</param>
        /// <param name="level">日志级别</param>
        /// <returns></returns>
        public override Castle.Core.Logging.ILogger Create(string name, LoggerLevel level) {
            //日志级别不能再运行时设置。请检查您的配置文件。
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="name">日志名</param>
        /// <returns></returns>
        public override Castle.Core.Logging.ILogger Create(string name) {
            return new OrchardLog4netLogger(LogManager.GetLogger(name), this);
        }
    }
}
