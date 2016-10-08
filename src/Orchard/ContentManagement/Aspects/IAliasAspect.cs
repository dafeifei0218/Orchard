namespace Orchard.ContentManagement.Aspects {
    /// <summary>
    /// 别名切面接口
    /// </summary>
    public interface IAliasAspect : IContent {
        /// <summary>
        /// 路径
        /// </summary>
        string Path { get; }
    }
}
