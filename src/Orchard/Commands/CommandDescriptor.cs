using System.Reflection;

namespace Orchard.Commands {
    /// <summary>
    /// 命令描述
    /// </summary>
    public class CommandDescriptor {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
        /// <summary>
        /// 帮助文本
        /// </summary>
        public string HelpText { get; set; }
    }
}