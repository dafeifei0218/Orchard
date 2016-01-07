using System;

namespace Orchard.Logging {
    /// <summary>
    /// ����־
    /// </summary>
    public class NullLogger : ILogger {
        private static readonly ILogger _instance = new NullLogger();

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        public static ILogger Instance {
            get { return _instance; }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="level">��־����</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level) {
            return false;
        }

        /// <summary>
        /// ��¼��־
        /// </summary>
        /// <param name="level">����</param>
        /// <param name="exception">�쳣</param>
        /// <param name="format">��ʽ</param>
        /// <param name="args">���󼯺�</param>
        public void Log(LogLevel level, Exception exception, string format, params object[] args) {
        }
    }
}