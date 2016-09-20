using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Orchard.Logging;
using Orchard.Exceptions;

namespace Orchard.Caching {
    /// <summary>
    /// 默认异步令牌提供者
    /// </summary>
    public class DefaultAsyncTokenProvider : IAsyncTokenProvider {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultAsyncTokenProvider() {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public IVolatileToken GetToken(Action<Action<IVolatileToken>> task) {
            var token = new AsyncVolativeToken(task, Logger);
            token.QueueWorkItem();
            return token;
        }

        /// <summary>
        /// 异步挥发令牌
        /// </summary>
        public class AsyncVolativeToken : IVolatileToken {
            private readonly Action<Action<IVolatileToken>> _task;
            private readonly List<IVolatileToken> _taskTokens = new List<IVolatileToken>();
            private volatile Exception _taskException;
            private volatile bool _isTaskFinished;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="task"></param>
            /// <param name="logger"></param>
            public AsyncVolativeToken(Action<Action<IVolatileToken>> task, ILogger logger) {
                _task = task;
                Logger = logger;
            }

            /// <summary>
            /// 日志
            /// </summary>
            public ILogger Logger { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public void QueueWorkItem() {
                // Start a work item to collect tokens in our internal array
                ThreadPool.QueueUserWorkItem(state => {
                    try {
                        _task(token => _taskTokens.Add(token));
                    }
                    catch (Exception ex) {
                        if (ex.IsFatal()) {                 
                            throw;
                        }
                        Logger.Error(ex, "Error while monitoring extension files. Assuming extensions are not current.");
                        _taskException = ex;
                    }
                    finally {
                        _isTaskFinished = true;
                    }
                });
            }

            /// <summary>
            /// 
            /// </summary>
            public bool IsCurrent {
                get {
                    // We are current until the task has finished
                    if (_taskException != null) {
                        return false;
                    }
                    if (_isTaskFinished) {
                        return _taskTokens.All(t => t.IsCurrent);
                    }
                    return true;
                }
            }
        }
    }
}