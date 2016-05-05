using System.Linq;
using Autofac;
using Autofac.Core;

namespace Orchard.Commands {
    /// <summary>
    /// 命令模块
    /// </summary>
    public class CommandModule : Module {
        /// <summary>
        /// 附加
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration) {

            if (!registration.Services.Contains(new TypedService(typeof(ICommandHandler))))
                return;

            var builder = new CommandHandlerDescriptorBuilder();
            var descriptor = builder.Build(registration.Activator.LimitType);
            registration.Metadata.Add(typeof(CommandHandlerDescriptor).FullName, descriptor);
        }
    }
}
