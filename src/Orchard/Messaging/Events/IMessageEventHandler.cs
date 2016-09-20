using System;
using Orchard.Events;
using Orchard.Messaging.Models;

namespace Orchard.Messaging.Events {
    /// <summary>
    /// 
    /// </summary>
    [Obsolete]
    public interface IMessageEventHandler : IEventHandler {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Sending(MessageContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Sent(MessageContext context);
    }
}
