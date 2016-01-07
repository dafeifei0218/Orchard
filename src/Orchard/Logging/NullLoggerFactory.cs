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
        /// <returns>���ؿ���־����</returns>
        public ILogger CreateLogger(Type type) {
            return NullLogger.Instance;
        }
    }
}