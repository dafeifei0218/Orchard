namespace Orchard {
    /// <summary>
    /// ӳ��ӿ�
    /// </summary>
    /// <typeparam name="TSource">Դ</typeparam>
    /// <typeparam name="TTarget">Ŀ��</typeparam>
    public interface IMapper<TSource, TTarget> : IDependency {
        /// <summary>
        /// ӳ��
        /// </summary>
        /// <param name="source">Դ</param>
        /// <returns>Ŀ��</returns>
        TTarget Map(TSource source);
    }
}