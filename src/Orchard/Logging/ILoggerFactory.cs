using System;

namespace Orchard.Logging {
    /// <summary>
    /// ��־�����ӿ�
    /// </summary>
    public interface ILoggerFactory {
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="type">����</param>
        /// <returns>��־�ӿ�</returns>
        ILogger CreateLogger(Type type);
    }
}