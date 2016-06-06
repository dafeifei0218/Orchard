namespace Orchard {
    /// <summary>
    /// 映射
    /// </summary>
    /// <typeparam name="TSource">源</typeparam>
    /// <typeparam name="TTarget">目标</typeparam>
    public interface IMapper<TSource, TTarget> : IDependency {
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="source">源</param>
        /// <returns></returns>
        TTarget Map(TSource source);
    }
}