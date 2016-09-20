using System;

namespace Orchard.Commands {
    /// <summary>
    /// Orchard开关自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OrchardSwitchAttribute : Attribute {
    }
}
