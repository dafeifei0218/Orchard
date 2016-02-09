using System;

namespace Orchard.Logging {
    /// <summary>
    /// ��־����
    /// </summary>
    public enum LogLevel {
        /// <summary>
        /// ����
        /// </summary>
        Debug,
        /// <summary>
        /// ��Ϣ
        /// </summary>
        Information,
        /// <summary>
        /// ����
        /// </summary>
        Warning,
        /// <summary>
        /// ����
        /// </summary>
        Error,
        /// <summary>
        /// ����
        /// </summary>
        Fatal
    }

    /// <summary>
    /// ��־�ӿ�
    /// </summary>
    public interface ILogger {
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="level">��־����</param>
        /// <returns>true�����ã�false��������</returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// ��¼��־
        /// </summary>
        /// <param name="level">��־����</param>
        /// <param name="exception">������Ϣ</param>
        /// <param name="format">��ʽ</param>
        /// <param name="args">���󼯺�</param>
        void Log(LogLevel level, Exception exception, string format, params object[] args);
    }
}
