using System;

namespace Orchard.Logging {
    /// <summary>
    /// Castle日志工厂
    /// </summary>
    public class CastleLoggerFactory : ILoggerFactory {
        //Castle日志工厂
        private readonly Castle.Core.Logging.ILoggerFactory _castleLoggerFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="castleLoggerFactory">Castle日志工厂</param>
        public CastleLoggerFactory(Castle.Core.Logging.ILoggerFactory castleLoggerFactory) {
            _castleLoggerFactory = castleLoggerFactory;
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type) {
            return new CastleLogger(_castleLoggerFactory.Create(type));
        }
    }
}
