using System.Collections.Generic;

namespace Orchard.Caching
{
    /// <summary>
    /// 信号接口
    /// </summary>
    public interface ISignals : IVolatileProvider
    {
        /// <summary>
        /// 触发一个信号量（设置IsCurrent为false导致缓存失效）。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="signal">信号</param>
        void Trigger<T>(T signal);

        /// <summary>
        /// 生成一个令牌
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="signal">信号</param>
        /// <returns></returns>
        IVolatileToken When<T>(T signal);
    }

    /// <summary>
    /// 信号，实现ISignals信号接口
    /// </summary>
    public class Signals : ISignals
    {
        readonly IDictionary<object, Token> _tokens = new Dictionary<object, Token>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="signal">信号</param>
        public void Trigger<T>(T signal)
        {
            lock (_tokens)
            {
                Token token;
                if (_tokens.TryGetValue(signal, out token))
                {
                    _tokens.Remove(signal);
                    token.Trigger();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="signal">信号</param>
        /// <returns></returns>
        public IVolatileToken When<T>(T signal)
        {
            lock (_tokens)
            {
                Token token;
                if (!_tokens.TryGetValue(signal, out token))
                {
                    token = new Token();
                    _tokens[signal] = token;
                }
                return token;
            }
        }

        /// <summary>
        /// 令牌
        /// </summary>
        class Token : IVolatileToken
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public Token()
            {
                IsCurrent = true;
            }
            /// <summary>
            /// 是否当前
            /// </summary>
            public bool IsCurrent { get; private set; }
            /// <summary>
            /// 触发，设置IsCurrent=  false
            /// </summary>
            public void Trigger() { IsCurrent = false; }
        }
    }
}
