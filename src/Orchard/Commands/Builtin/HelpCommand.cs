using System;
using System.Linq;
using Orchard.Localization;

namespace Orchard.Commands.Builtin {
    /// <summary>
    /// 帮助命令
    /// </summary>
    public class HelpCommand : DefaultOrchardCommandHandler {
        private readonly ICommandManager _commandManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commandManager">命令管理</param>
        public HelpCommand(ICommandManager commandManager) {
            _commandManager = commandManager;
        }

        /// <summary>
        /// 全部命令
        /// </summary>
        [CommandName("help commands")]
        [CommandHelp("help commands\r\n\t" + "Display help text for all available commands")]
        public void AllCommands() {
            Context.Output.WriteLine(T("List of available commands:"));
            Context.Output.WriteLine(T("---------------------------"));
            Context.Output.WriteLine();

            var descriptors = _commandManager.GetCommandDescriptors().OrderBy(d => d.Name);
            foreach (var descriptor in descriptors) {
                Context.Output.WriteLine(GetHelpText(descriptor));
                Context.Output.WriteLine();
            }
        }

        /// <summary>
        /// 获取帮助文本
        /// </summary>
        /// <param name="descriptor">命令描述</param>
        /// <returns></returns>
        private LocalizedString GetHelpText(CommandDescriptor descriptor) {
            if (string.IsNullOrEmpty(descriptor.HelpText)) {
                return T("{0}: no help text",
                         descriptor.MethodInfo.DeclaringType.FullName + "." + descriptor.MethodInfo.Name);
            }

            return T(descriptor.HelpText);
        }

        /// <summary>
        /// 单命令
        /// </summary>
        /// <param name="commandNameStrings">命令名称字符串</param>
        [CommandName("help")]
        [CommandHelp("help <command>\r\n\t" + "Display help text for <command>")]
        public void SingleCommand(string[] commandNameStrings) {
            string command = string.Join(" ", commandNameStrings);
            var descriptors = _commandManager.GetCommandDescriptors().Where(t => t.Name.StartsWith(command, StringComparison.OrdinalIgnoreCase)).OrderBy(d => d.Name);
            if (!descriptors.Any()) {
                Context.Output.WriteLine(T("Command {0} doesn't exist").ToString(), command);
            }
            else {
                foreach (var descriptor in descriptors) {
                    Context.Output.WriteLine(GetHelpText(descriptor));
                    Context.Output.WriteLine();
                }
            }
        }
    }
}
