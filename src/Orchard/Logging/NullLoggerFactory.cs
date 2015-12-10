using System;

namespace Orchard.Logging {
    /// <summary>
    /// ����־����
    /// </summary>
    class NullLoggerFactory : ILoggerFactory {
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type) {
            return NullLogger.Instance;
        }
    }
}