namespace Orchard {
    /// <summary>
    /// ӳ��
    /// </summary>
    /// <typeparam name="TSource">Դ</typeparam>
    /// <typeparam name="TTarget">Ŀ��</typeparam>
    public interface IMapper<TSource, TTarget> : IDependency {
        TTarget Map(TSource source);
    }
}