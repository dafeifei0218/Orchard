namespace Orchard.Commands {
    /// <summary>
    /// 命令处理程序接口
    /// </summary>
    public interface ICommandHandler : IDependency {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context">命令处理上下文</param>
        void Execute(CommandContext context);
    }
}
