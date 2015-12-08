namespace Orchard {
    /// <summary>
    /// Ó³Éä
    /// </summary>
    /// <typeparam name="TSource">Ô´</typeparam>
    /// <typeparam name="TTarget">Ä¿±ê</typeparam>
    public interface IMapper<TSource, TTarget> : IDependency {
        TTarget Map(TSource source);
    }
}