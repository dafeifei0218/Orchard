using System.Reflection;
using Orchard.Environment;
using Orchard.Localization;

namespace Orchard.Commands {
    /// <summary>
    /// 
    /// </summary>
    internal class CommandHostEnvironment : HostEnvironment {
        /// <summary>
        /// 
        /// </summary>
        public CommandHostEnvironment() {
            T = NullLocalizer.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public Localizer T { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void RestartAppDomain() {
            throw new OrchardCommandHostRetryException(T("A change of configuration requires the session to be restarted."));
        }
    }
}