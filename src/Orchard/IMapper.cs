namespace Orchard {
    /// <summary>
    /// ӳ��
    /// </summary>
    /// <typeparam name="TSource">Դ</typeparam>
    /// <typeparam name="TTarget">Ŀ��</typeparam>
    public interface IMapper<TSource, TTarget> : IDependency {
        /// <summary>
        /// ӳ��
        /// </summary>
        /// <param name="source">Դ</param>
        /// <returns></returns>
        TTarget Map(TSource source);
    }
}