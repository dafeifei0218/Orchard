using System;

namespace Orchard.Caching {
    /// <summary>
    /// 弱引用
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class Weak<T> {
        private readonly WeakReference _target;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="target">目标类型</param>
        public Weak(T target) {
            _target = new WeakReference(target);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="target">目标类型</param>
        /// <param name="trackResurrection">追踪复活</param>
        public Weak(T target, bool trackResurrection) {
            _target = new WeakReference(target, trackResurrection);
        }

        /// <summary>
        /// 目标类型
        /// </summary>
        public T Target {
            get { return (T)_target.Target; }
            set { _target.Target = value; }
        }
    }
}