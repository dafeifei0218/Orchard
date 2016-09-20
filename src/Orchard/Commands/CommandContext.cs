using System.Collections.Generic;
using System.IO;

namespace Orchard.Commands {
    /// <summary>
    /// 命令上下文
    /// </summary>
    public class CommandContext {
        /// <summary>
        /// 输入
        /// </summary>
        public TextReader Input { get; set; }
        /// <summary>
        /// 输出
        /// </summary>
        public TextWriter Output { get; set; }


        /// <summary>
        /// 命令名称
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 参数集合
        /// </summary>
        public IEnumerable<string> Arguments { get; set; }
        /// <summary>
        /// 开关集合
        /// </summary>
        public IDictionary<string,string> Switches { get; set; }

        /// <summary>
        /// 命令描述
        /// </summary>
        public CommandDescriptor CommandDescriptor { get; set; }
    }
}
