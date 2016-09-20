using System.Collections.Generic;
using System.IO;

namespace Orchard.Commands {
    /// <summary>
    /// 命令参数
    /// </summary>
    public class CommandParameters {
        /// <summary>
        /// 参数集合
        /// </summary>
        public IEnumerable<string> Arguments { get; set; }
        /// <summary>
        /// 开关字段字典
        /// </summary>
        public IDictionary<string, string> Switches { get; set; }

        /// <summary>
        /// 输入
        /// </summary>
        public TextReader Input { get; set; }
        /// <summary>
        /// 输出
        /// </summary>
        public TextWriter Output { get; set; }
    }
}
