namespace Orchard.Settings {
    /// <summary>
    /// 站点服务接口
    /// </summary>
    public interface ISiteService : IDependency {
        /// <summary>
        /// 获取站点设置
        /// </summary>
        /// <returns></returns>
        ISite GetSiteSettings();
    }
}
