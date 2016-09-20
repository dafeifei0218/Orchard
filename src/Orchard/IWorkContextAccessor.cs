using System;
using System.Web;

namespace Orchard {
    /// <summary>
    /// 工作上下文访问器接口
    /// </summary>
    public interface IWorkContextAccessor {
        WorkContext GetContext(HttpContextBase httpContext);
        IWorkContextScope CreateWorkContextScope(HttpContextBase httpContext);

        WorkContext GetContext();
        IWorkContextScope CreateWorkContextScope();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IWorkContextStateProvider : IDependency {
        Func<WorkContext, T> Get<T>(string name);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IWorkContextScope : IDisposable {
        WorkContext WorkContext { get; }
        TService Resolve<TService>();
        bool TryResolve<TService>(out TService service);
    }
}
