using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace Orchard.WarmupStarter {
    /// <summary>
    /// 启动初始化模块
    /// </summary>
    public class WarmupHttpModule : IHttpModule {

        private HttpApplication _context;
        private static object _synLock = new object();
        private static IList<Action> _awaiting = new List<Action>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">应用程序上下文</param>
        public void Init(HttpApplication context) {
            _context = context;
            context.AddOnBeginRequestAsync(BeginBeginRequest, EndBeginRequest, null);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static bool InWarmup() {
            lock (_synLock) {
                return _awaiting != null;
            }
        }

        /// <summary>
        /// Warmup code is about to start: Any new incoming request is queued 
        /// until "SignalWarmupDone" is called.
        /// </summary>
        public static void SignalWarmupStart() {
            lock (_synLock) {
                if (_awaiting == null) {
                    _awaiting = new List<Action>();
                }
            }
        }

        /// <summary>
        /// Warmup code just completed: All pending requests in the "_await" queue are processed, 
        /// and any new incoming request is now processed immediately.
        /// 启动代码刚要完成：在“_await”队列进行处理，所有挂起的请求和任何新的传入请求现在立即处理。
        /// </summary>
        public static void SignalWarmupDone() {
            IList<Action> temp;

            lock (_synLock) {
                temp = _awaiting;
                _awaiting = null;
            }

            if (temp != null) {
                foreach (var action in temp) {
                    action();
                }
            }
        }

        /// <summary>
        /// Enqueue or directly process action depending on current mode.
        /// 入队或直接作用取决于电流模式。
        /// </summary>
        private void Await(Action action) {
            Action temp = action;

            lock (_synLock) {
                if (_awaiting != null) {
                    temp = null;
                    _awaiting.Add(action);
                }
            }

            if (temp != null) {
                temp();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="cb"></param>
        /// <param name="extradata"></param>
        /// <returns></returns>
        private IAsyncResult BeginBeginRequest(object sender, EventArgs e, AsyncCallback cb, object extradata) {
            // host is available, process every requests, or file is processed
            // 主机是可用的，过程中的每一个要求，或文件处理
            if (!InWarmup() || WarmupUtility.DoBeginRequest(_context)) {
                var asyncResult = new DoneAsyncResult(extradata);
                cb(asyncResult);
                return asyncResult;
            }
            else {
                // this is the "on hold" execution path
                // 这是“搁置”的执行路径
                var asyncResult = new WarmupAsyncResult(cb, extradata);
                Await(asyncResult.Completed);
                return asyncResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private static void EndBeginRequest(IAsyncResult ar) {
        }

        /// <summary>
        /// AsyncResult for "on hold" request (resumes when "Completed()" is called)
        /// 异步结果“搁置”的请求（简历当“完成”之称）
        /// </summary>
        private class WarmupAsyncResult : IAsyncResult {
            private readonly EventWaitHandle _eventWaitHandle = new AutoResetEvent(false/*initialState*/);
            private readonly AsyncCallback _cb;
            private readonly object _asyncState;
            private bool _isCompleted;

            public WarmupAsyncResult(AsyncCallback cb, object asyncState) {
                _cb = cb;
                _asyncState = asyncState;
                _isCompleted = false;
            }

            public void Completed() {
                _isCompleted = true;
                _eventWaitHandle.Set();
                _cb(this);
            }

            bool IAsyncResult.CompletedSynchronously {
                get { return false; }
            }

            bool IAsyncResult.IsCompleted {
                get { return _isCompleted; }
            }

            object IAsyncResult.AsyncState {
                get { return _asyncState; }
            }

            WaitHandle IAsyncResult.AsyncWaitHandle {
                get { return _eventWaitHandle; }
            }
        }

        /// <summary>
        /// Async result for "ok to process now" requests
        /// 异步结果“好的过程”请求
        /// </summary>
        private class DoneAsyncResult : IAsyncResult {
            private readonly object _asyncState;
            private static readonly WaitHandle _waitHandle = new ManualResetEvent(true/*initialState*/);

            public DoneAsyncResult(object asyncState) {
                _asyncState = asyncState;
            }

            bool IAsyncResult.CompletedSynchronously {
                get { return true; }
            }

            bool IAsyncResult.IsCompleted {
                get { return true; }
            }

            WaitHandle IAsyncResult.AsyncWaitHandle {
                get { return _waitHandle; }
            }

            object IAsyncResult.AsyncState {
                get { return _asyncState; }
            }
        }
    }
}