using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Orchard.Environment;
using Orchard.Events;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Security;
using Orchard.UI.Notify;

namespace Orchard.Exceptions {
    /// <summary>
    /// 默认异常策略
    /// </summary>
    public class DefaultExceptionPolicy : IExceptionPolicy {
        private readonly INotifier _notifier;
        private readonly Work<IAuthorizer> _authorizer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultExceptionPolicy() {
            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifier"></param>
        /// <param name="authorizer"></param>
        public DefaultExceptionPolicy(INotifier notifier, Work<IAuthorizer> authorizer) {
            _notifier = notifier;
            _authorizer = authorizer;
            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Localizer T { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public bool HandleException(object sender, Exception exception) {
            if (IsFatal(exception)) {
                return false;
            }

            if (sender is IEventBus &&  exception is OrchardFatalException) {
                return false;
            }

            Logger.Error(exception, "An unexpected exception was caught");

            do {
                RaiseNotification(exception);
                exception = exception.InnerException;
            } while (exception != null);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static bool IsFatal(Exception exception) {
            return 
                exception is OrchardSecurityException ||
                exception is StackOverflowException ||
                exception is AccessViolationException ||
                exception is AppDomainUnloadedException ||
                exception is ThreadAbortException ||
                exception is SecurityException ||
                exception is SEHException;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        private void RaiseNotification(Exception exception) {
            if (_notifier == null || _authorizer.Value == null) {
                return;
            }
            if (exception is OrchardException) {
                _notifier.Error((exception as OrchardException).LocalizedMessage);
            }
            else if (_authorizer.Value.Authorize(StandardPermissions.SiteOwner)) {
                _notifier.Error(T(exception.Message));
            }
        }
    }
}
