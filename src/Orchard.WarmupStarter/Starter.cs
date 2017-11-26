using System;
using System.Threading;
using System.Web;

namespace Orchard.WarmupStarter
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Starter<T> where T : class
    {
        //由于是新开线程进行初始化操作，在初始化过程中，第一次请求会在管道中继续进行,这时候也有可能会有新的请求进入。
        //如果在初始化操作完成之前，任由这些请求进行下去，很可能得不到要想的结果。
        //所以Orchard提供了一个异步HttpMoudle，即 Orchard.WarmupStarter.WarmupHttpModule。
        //在初始化正在进行时，将请求的异步BeginRequest处理"暂停"在那儿，等初始化完成后(不管失败与否)，让异步BeginRequest处理完成。
        //在初始化的过程中如果有异常发生，则会将异常记录下来。

        private readonly Func<HttpApplication, T> _initialization;
        private readonly Action<HttpApplication, T> _beginRequest;
        private readonly Action<HttpApplication, T> _endRequest;
        private readonly object _synLock = new object();
        /// <summary>
        /// The result of the initialization queued work item.
        /// Set only when initialization has completed without errors.
        /// 初始化队列工作项的结果。
        /// 只有当初始化完成没有错误设置。
        /// </summary>
        private volatile T _initializationResult;
        /// <summary>
        /// The (potential) error raised by the initialization thread. This is a "one-time"
        /// error signal, so that we can restart the initialization once another request
        /// comes in.
        /// 初始化线程引发的（潜在的）错误。
        /// 这是一个“一次性”错误信号，一旦另一个请求进来，这样我们可以重新启动的初始化，
        /// </summary>
        private volatile Exception _error;
        /// <summary>
        /// The (potential) error from the previous initiazalition. We need to
        /// keep this error active until the next initialization is finished,
        /// so that we can keep reporting the error for all incoming requests.
        /// 从先前的初始化线程（潜在的）错误。
        /// 我们需要保持这个错误，知道下一个初始化完成，因此我们可以继续报告所有传入请求的错误。
        /// </summary>
        private volatile Exception _previousError;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialization"></param>
        /// <param name="beginRequest">开始请求</param>
        /// <param name="endRequest">结束请求</param>
        public Starter(Func<HttpApplication, T> initialization, Action<HttpApplication, T> beginRequest, Action<HttpApplication, T> endRequest)
        {
            _initialization = initialization;
            _beginRequest = beginRequest;
            _endRequest = endRequest;
        }

        /// <summary>
        /// 当应用程序启动时执行此方法
        /// </summary>
        /// <param name="application">ASP.NET应用程序对象</param>
        public void OnApplicationStart(HttpApplication application)
        {            
            LaunchStartupThread(application);
        }

        /// <summary>
        /// 开始请求时
        /// </summary>
        /// <param name="application">ASP.NET应用程序对象</param>
        public void OnBeginRequest(HttpApplication application)
        {
            //在异步BeginRequest事件处理完成后，将处理同步BeginRequest事件。
            //事件处理程序将检查上一次初始化请求是否有异常发生；
            //如果检查到有异常发生，则会再次执行LaunchStartupThread方法尝试新的初始化操作；
            //如果新的初始化没有异常发生，就"忘记"上次初始化出现过异常，否则将本次异常进行记录，抛出上次初始化异常。
            //注意：在再次执行LaunchStartupThread方法时，如果有新的请求进入，也会将请求的异步BeginRequest处理"暂停"在那里，直到初始化完成。
            //请查看Starter<T> 的OnBeginRequest方法的代码：

            // Initialization resulted in an error
            //初始化导致的错误
            if (_error != null)
            {
                // Save error for next requests and restart async initialization.
                // Note: The reason we have to retry the initialization is that the 
                //       application environment may change between requests,
                //       e.g. App_Data is made read-write for the AppPool.
                //下次请求和启动异步初始化，保存错误。
                //注：我们必须重试的原因是：应用环境可能会改变请求，例如：App_Data进行读写的AppPool。
                bool restartInitialization = false;

                lock (_synLock)
                {
                    if (_error != null)
                    {
                        _previousError = _error;
                        _error = null;
                        restartInitialization = true;
                    }
                }

                if (restartInitialization)
                {
                    LaunchStartupThread(application);
                }
            }

            // Previous initialization resulted in an error (and another initialization is running)
            //先前的初始化导致的错误（和另一个正在运行的初始化）
            if (_previousError != null)
            {
                throw new ApplicationException("Error during application initialization", _previousError);
            }

            // Only notify if the initialization has successfully completed
            // 如果初始化已成功完成，结果不为空，只有通知。
            if (_initializationResult != null)
            {
                _beginRequest(application, _initializationResult);
            }
        }

        /// <summary>
        /// 结束请求时
        /// </summary>
        /// <param name="application">ASP.NET应用程序对象</param>
        public void OnEndRequest(HttpApplication application)
        {
            // Only notify if the initialization has successfully completed
            // 如果已经成功初始化的通知
            if (_initializationResult != null)
            {
                _endRequest(application, _initializationResult);
            }
        }

        /// <summary>
        /// Run the initialization delegate asynchronously in a queued work item
        /// 通过线程池启动一个新的线程进行异步初始化操作
        /// </summary>
        /// <param name="application">ASP.NET应用程序对象</param>
        public void LaunchStartupThread(HttpApplication application)
        {
            // Make sure incoming requests are queued
            // 确保输入的请求队列
            
            //LaunchStartupThread方法首先调用WarmupHttpModule的静态方法SignalWarmupStart，
            //用于初始化一个静态List<Action>列表，该列表保存异步结果WarmupAsyncResult类的Completed方法。
            //在初始化完成后，不管成功与否，Completed方法都将会得到调用以保证"暂停"在那的异步BeginRequest处理完成，
            //即WarmupHttpModule的静态方法SignalWarmupDone。
            WarmupHttpModule.SignalWarmupStart();

            ThreadPool.QueueUserWorkItem(
                state =>
                {
                    try
                    {
                        var result = _initialization(application);
                        _initializationResult = result;
                    }
                    catch (Exception ex)
                    {
                        lock (_synLock)
                        {
                            _error = ex;
                            _previousError = null;
                        }
                    }
                    finally
                    {
                        // Execute pending requests as the initialization is over
                        // 当初始化结束时执行待定的请求
                        WarmupHttpModule.SignalWarmupDone();
                    }
                });
        }
    }
}
