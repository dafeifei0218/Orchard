using System;

namespace Orchard.Caching {
    /// <summary>
    /// 异步令牌提供者
    /// </summary>
    public interface IAsyncTokenProvider {
        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        IVolatileToken GetToken(Action<Action<IVolatileToken>> task);
    }
}