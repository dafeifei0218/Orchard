using System.Collections.Generic;

namespace Orchard.Commands {
    /// <summary>
    /// 命令处理管理接口
    /// </summary>
    public interface ICommandManager : IDependency {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameters">命令处理参数</param>
        void Execute(CommandParameters parameters);
        /// <summary>
        /// 获取命令描述列表
        /// </summary>
        /// <returns>命令描述集合</returns>
        IEnumerable<CommandDescriptor> GetCommandDescriptors();
    }
}