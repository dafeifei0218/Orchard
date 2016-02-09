using System;

namespace Orchard.Logging {
    /// <summary>
    /// Castle��־����
    /// </summary>
    public class CastleLoggerFactory : ILoggerFactory {
        //Castle��־����
        private readonly Castle.Core.Logging.ILoggerFactory _castleLoggerFactory;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="castleLoggerFactory">Castle��־����</param>
        public CastleLoggerFactory(Castle.Core.Logging.ILoggerFactory castleLoggerFactory) {
            _castleLoggerFactory = castleLoggerFactory;
        }

        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type) {
            return new CastleLogger(_castleLoggerFactory.Create(type));
        }
    }
}
