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
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public OrchardCommandHostRetryException(LocalizedString message)
            : base(message) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public OrchardCommandHostRetryException(LocalizedString message, Exception innerException)
            : base(message, innerException) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">存储将对象序列化或反序列化所需的全部数据。</param>
        /// <param name="context">描述给定的序列化流的源和目标，并提供一个由调用方定义的附加上下文。</param>
        protected OrchardCommandHostRetryException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}