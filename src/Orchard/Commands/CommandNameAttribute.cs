using System;

namespace Orchard.Commands {
    /// <summary>
    /// 命令名称自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandNameAttribute : Attribute {
        private readonly string _commandAlias;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commandAlias">命令别名</param>
        public CommandNameAttribute(string commandAlias) {
            _commandAlias = commandAlias;
        }

        /// <summary>
        /// 命令
        /// </summary>
        public string Command {
            get { return _commandAlias; }
        }
    }

    /// <summary>
    /// 命令帮助自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandHelpAttribute : Attribute {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">帮助文本</param>
        public CommandHelpAttribute(string text) {
            this.HelpText = text;
        }

        /// <summary>
        /// 帮助文本
        /// </summary>
        public string HelpText { get; set; }
    }
}
