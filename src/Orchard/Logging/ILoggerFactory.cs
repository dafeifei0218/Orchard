using System;

namespace Orchard.Logging {
    /// <summary>
    /// ��־����
    /// </summary>
    public interface ILoggerFactory {
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        ILogger CreateLogger(Type type);
    }
}