using System;
using System.Runtime.Serialization;
using Orchard.Localization;

namespace Orchard {
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OrchardException : ApplicationException {
        //
        private readonly LocalizedString _localizedMessage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public OrchardException(LocalizedString message)
            : base(message.Text) {
            _localizedMessage = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public OrchardException(LocalizedString message, Exception innerException)
            : base(message.Text, innerException) {
            _localizedMessage = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected OrchardException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

        /// <summary>
        /// 
        /// </summary>
        public LocalizedString LocalizedMessage { get { return _localizedMessage; } }
    }
}