namespace Orchard.ContentManagement.Aspects {
    /// <summary>
    /// 本地化切面接口
    /// </summary>
    public interface ILocalizableAspect : IContent {
        /// <summary>
        /// 语言文化
        /// </summary>
        string Culture { get ; }
    }
}
