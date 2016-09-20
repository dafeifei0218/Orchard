using System;
using System.Collections.Generic;
using System.Linq;

namespace Orchard.Commands {
    /// <summary>
    /// Orchard开关自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class OrchardSwitchesAttribute : Attribute {
        private readonly string _switches;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="switches">开关名称</param>
        public OrchardSwitchesAttribute(string switches) {
            _switches = switches;
        }

        /// <summary>
        /// 开关名称集合
        /// </summary>
        public IEnumerable<string> Switches {
            get {
                return (_switches ?? "").Trim().Split(',').Select(s => s.Trim());
            }
        }
    }
}
