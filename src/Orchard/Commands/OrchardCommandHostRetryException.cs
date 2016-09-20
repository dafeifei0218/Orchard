using System;
using System.Runtime.Serialization;
using Orchard.Localization;

namespace Orchard.Commands {
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrchardCommandHostRetryException : OrchardCoreException {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public OrchardCommandHostRetryException(LocalizedString message)
            : base(message) {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="innerException">�ڲ��쳣</param>
        public OrchardCommandHostRetryException(LocalizedString message, Exception innerException)
            : base(message, innerException) {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="info">�洢���������л������л������ȫ�����ݡ�</param>
        /// <param name="context">�������������л�����Դ��Ŀ�꣬���ṩһ���ɵ��÷�����ĸ��������ġ�</param>
        protected OrchardCommandHostRetryException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}