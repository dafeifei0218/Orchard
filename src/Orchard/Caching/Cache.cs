using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Orchard.Caching {    
    /// <summary>
    /// 缓存
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TResult">结果</typeparam>
    public class Cache<TKey, TResult> : ICache<TKey, TResult> {
        //缓存上下文访问器
        private readonly ICacheContextAccessor _cacheContextAccessor;
        //线程安全的字典表，用于存储缓存的数据
        private readonly ConcurrentDictionary<TKey, CacheEntry> _entries;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheContextAccessor"></param>
        public Cache(ICacheContextAccessor cacheContextAccessor) {
            _cacheContextAccessor = cacheContextAccessor;
            _entries = new ConcurrentDictionary<TKey, CacheEntry>();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public TResult Get(TKey key, Func<AcquireContext<TKey>, TResult> acquire) {
            var entry = _entries.AddOrUpdate(key,
                // "Add" lambda
                k => AddEntry(k, acquire),
                // "Update" lambda
                (k, currentEntry) => UpdateEntry(currentEntry, k, acquire));

            return entry.Result;
        }

        /// <summary>
        /// 添加缓存条目
        /// </summary>
        /// <param name="k">键</param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        private CacheEntry AddEntry(TKey k, Func<AcquireContext<TKey>, TResult> acquire) {
            //创建一个缓存条目
            var entry = CreateEntry(k, acquire);
            //传播令牌
            PropagateTokens(entry);
            return entry;
        }

        /// <summary>
        /// 更新缓存条目
        /// </summary>
        /// <param name="currentEntry">当前缓存条目</param>
        /// <param name="k">键</param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        private CacheEntry UpdateEntry(CacheEntry currentEntry, TKey k, Func<AcquireContext<TKey>, TResult> acquire) {
            //遍历缓存条目中所有的令牌，如果其中一个令牌表示为缓存失效则重新创建缓存条目，否则返回当前缓存条目
            //注意看(currentEntry.Tokens.Any(t => t != null && !t.IsCurrent))缓存失效的核心就在这里了，
            //实现了IVolatileToken接口的对象是一个引用类型，只要这个实例被添加至对应的缓存条目，
            //并且通过一些手段将IsCurrent设为False那么这个缓存就失效了。
            var entry = (currentEntry.Tokens.Any(t => t != null && !t.IsCurrent)) ? CreateEntry(k, acquire) : currentEntry;
            //传播令牌
            PropagateTokens(entry);
            return entry;
        }

        /// <summary>
        /// 传播令牌
        /// </summary>
        /// <param name="entry">缓存条目</param>
        private void PropagateTokens(CacheEntry entry) {
            // Bubble up volatile tokens to parent context
            if (_cacheContextAccessor.Current != null) {
                foreach (var token in entry.Tokens)
                    _cacheContextAccessor.Current.Monitor(token);
            }
        }

        /// <summary>
        /// 创建缓存条目
        /// </summary>
        /// <param name="k"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        private CacheEntry CreateEntry(TKey k, Func<AcquireContext<TKey>, TResult> acquire) {
            //创建一个空的缓存条目
            var entry = new CacheEntry();
            //创建一个新的获取上下文
            //在上面提到 ctx.Monitor(_signals.When("culturesChanged"));那么Monitor方法就是把When方法返回的IVolatileToken接口的实例添加到缓存条目中的令牌集合中
            var context = new AcquireContext<TKey>(k, entry.AddToken);

            //生命一个变量用于存储之前已经存在的获取上下文
            IAcquireContext parentContext = null;
            try {
                // Push context
                //将之前的获取上下文进行保存
                parentContext = _cacheContextAccessor.Current;
                //设置新的获取上下文为新的
                _cacheContextAccessor.Current = context;

                //得到缓冲的结果（执行Get方法中传入的acquire参数）
                entry.Result = acquire(context);
            }
            finally {
                // Pop context
                //不论缓存条目是否添加成功都还原之前的获取上下文
                _cacheContextAccessor.Current = parentContext;
            }
            //压缩令牌、剔除重复的令牌实现
            entry.CompactTokens();
            return entry;
        }

        /// <summary>
        /// 缓存条目，
        /// 对缓存结果进行了封装，主要对缓存结果添加令牌机制（IVolatileToken）。
        /// </summary>
        private class CacheEntry {
            //挥发令牌集合
            private IList<IVolatileToken> _tokens;
            /// <summary>
            /// 缓存结果
            /// </summary>
            public TResult Result { get; set; }

            /// <summary>
            /// 挥发令牌集合
            /// </summary>
            public IEnumerable<IVolatileToken> Tokens {
                get {
                    return _tokens ?? Enumerable.Empty<IVolatileToken>();
                }
            }

            /// <summary>
            /// 添加一个新的令牌至Tokens。
            /// </summary>
            /// <param name="volatileToken">挥发令牌</param>
            public void AddToken(IVolatileToken volatileToken) {
                if (_tokens == null) {
                    _tokens = new List<IVolatileToken>();
                }

                _tokens.Add(volatileToken);
            }

            /// <summary>
            /// 主要用于去除Tokens中重复的令牌。
            /// （因为令牌是提供给外部添加的所有可能会出现重复的令牌，为提高性能(令牌内的执行执行时间不得而知)需要剔除重复的令牌）
            /// </summary>
            public void CompactTokens() {
                if (_tokens != null)
                    _tokens = _tokens.Distinct().ToArray();
            }
        }
    }
}
