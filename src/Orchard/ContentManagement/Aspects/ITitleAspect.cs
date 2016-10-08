namespace Orchard.ContentManagement.Aspects {
    /// <summary>
    /// 标题切面接口
    /// </summary>
    public interface ITitleAspect : IContent {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }
    }
}
