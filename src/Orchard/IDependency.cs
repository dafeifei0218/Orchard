using Orchard.Localization;
using Orchard.Logging;

namespace Orchard {
    /// <summary>
    /// Base interface for services that are instantiated per unit of work (i.e. web request).
    /// 依赖接口，这是实例化的工作单元服务基础接口（即：web请求）。
    /// </summary>
    public interface IDependency {
    }

    /// <summary>
    /// Base interface for services that are instantiated per shell/tenant.
    /// 单例依赖接口，这是每个shell/租户服务实例化基类接口
    /// </summary>
    public interface ISingletonDependency : IDependency {
    }

    /// <summary>
    /// Base interface for services that may *only* be instantiated in a unit of work.
    /// This interface is used to guarantee they are not accidentally referenced by a singleton dependency.
    /// 工作单元依赖接口
    /// </summary>
    public interface IUnitOfWorkDependency : IDependency {
    }

    /// <summary>
    /// Base interface for services that are instantiated per usage.
    /// 瞬态依赖接口，这是每次使用服务实例化接口。
    /// </summary>
    public interface ITransientDependency : IDependency {
    }


    /// <summary>
    /// 
    /// </summary>
    public abstract class Component : IDependency {
        protected Component() {
            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }
    }
}
