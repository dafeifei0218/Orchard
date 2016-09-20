using Orchard.Tasks;

namespace Orchard.Commands
{
    /// <summary>
    /// Command line specific "no-op" background service implementation.
    /// Note that we make this class "internal" so that it's not auto-registered
    /// by the Orchard Framework (it is registered explicitly by the command
    /// line host).
    /// 命令行特定的“无运算”的后台服务实现。
    /// 请注意，我们使这个类“内部”，使它不是由Orchard框架自动注册（它是由命令显式注册的线路主机）。
    /// </summary>
    internal class CommandBackgroundService : IBackgroundService {
        /// <summary>
        /// 
        /// </summary>
        public void Sweep() {
            // Don't run any background service in command line
            // 在命令行中不运行任何后台服务
        }
    }
}