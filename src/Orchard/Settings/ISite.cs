using Orchard.ContentManagement;

namespace Orchard.Settings {
    /// <summary>
    /// Interface provided by the "settings" model.
    /// 设置模型接口
    /// </summary>
    public interface ISite : IContent {
        /// <summary>
        /// 
        /// </summary>
        string PageTitleSeparator { get; }
        /// <summary>
        /// 站点名称
        /// </summary>
        string SiteName { get; }
        /// <summary>
        /// 
        /// </summary>
        string SiteSalt { get; }
        /// <summary>
        /// 
        /// </summary>
        string SuperUser { get; }
        /// <summary>
        /// 
        /// </summary>
        string HomePage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string SiteCulture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string SiteCalendar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        ResourceDebugMode ResourceDebugMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool UseCdn { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 最大页大小
        /// </summary>
        int MaxPageSize { get; set; }
        /// <summary>
        /// 最大页数
        /// </summary>
        int MaxPagedCount { get; set; }
        /// <summary>
        /// 基本Url
        /// </summary>
        string BaseUrl { get; }
        /// <summary>
        /// 
        /// </summary>
        string SiteTimeZone { get; }
    }
}
