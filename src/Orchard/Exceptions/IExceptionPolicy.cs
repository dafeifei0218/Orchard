using System;

namespace Orchard.Exceptions {
    /// <summary>
    /// 异常策略接口
    /// </summary>
    public interface IExceptionPolicy : ISingletonDependency {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="sender">发送对象</param>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        /* return false if the exception should be rethrown by the caller */
        bool HandleException(object sender, Exception exception);
    }
}
