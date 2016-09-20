using System;

namespace Orchard.Caching {
    /// <summary>
    /// 异步令牌提供者接口
    /// </summary>
    public interface IAsyncTokenProvider {
        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="task">任务</param>
        /// <returns>挥发令牌接口</returns>
        IVolatileToken GetToken(Action<Action<IVolatileToken>> task);
    }
}