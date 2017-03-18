namespace Orchard.ContentManagement.Aspects {
    /// <summary>
    /// 路由切面接口
    /// </summary>
    public interface IRoutableAspect : ITitleAspect, IAliasAspect {
        /// <summary>
        /// 塞
        /// </summary>
        string Slug { get; set; }
    }
}
