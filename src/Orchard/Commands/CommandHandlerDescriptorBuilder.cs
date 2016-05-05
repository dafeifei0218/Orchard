using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orchard.Commands {
    /// <summary>
    /// 命令处理描述建造者
    /// </summary>
    public class CommandHandlerDescriptorBuilder {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public CommandHandlerDescriptor Build(Type type) {
            return new CommandHandlerDescriptor { Commands = CollectMethods(type) };
        }

        /// <summary>
        /// 收集方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private IEnumerable<CommandDescriptor> CollectMethods(Type type) {
            var methods = type
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName);

            foreach (var methodInfo in methods) {
                yield return BuildMethod(methodInfo);
            }
        }

        /// <summary>
        /// 建造方法
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns></returns>
        private CommandDescriptor BuildMethod(MethodInfo methodInfo) {
            return new CommandDescriptor {
                                             Name = GetCommandName(methodInfo),
                                             MethodInfo = methodInfo,
                                             HelpText = GetCommandHelpText(methodInfo)
                                         };
        }

        /// <summary>
        /// 获取命令帮助文本
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns></returns>
        private string GetCommandHelpText(MethodInfo methodInfo) {
            var attributes = methodInfo.GetCustomAttributes(typeof(CommandHelpAttribute), false/*inherit*/);
            if (attributes != null && attributes.Any()) {
                return attributes.Cast<CommandHelpAttribute>().Single().HelpText;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取命名名称
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns></returns>
        private string GetCommandName(MethodInfo methodInfo) {
            var attributes = methodInfo.GetCustomAttributes(typeof(CommandNameAttribute), false/*inherit*/);
            if (attributes != null && attributes.Any()) {
                return attributes.Cast<CommandNameAttribute>().Single().Command;
            }

            return methodInfo.Name.Replace('_', ' ');
        }
    }
}
