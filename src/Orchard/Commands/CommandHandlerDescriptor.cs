using System.Collections.Generic;

namespace Orchard.Commands {
    /// <summary>
    /// 命令处理描述
    /// </summary>
    public class CommandHandlerDescriptor {
        /// <summary>
        /// 命令描述集合
        /// </summary>
        public IEnumerable<CommandDescriptor> Commands { get; set; }
    }
}
