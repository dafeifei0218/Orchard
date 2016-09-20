using System;

namespace Orchard.Settings {
    /// <summary>
    /// 
    /// </summary>
    public class CurrentSiteWorkContext : IWorkContextStateProvider {
        //
        private readonly ISiteService _siteService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteService"></param>
        public CurrentSiteWorkContext(ISiteService siteService) {
            _siteService = siteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public Func<WorkContext, T> Get<T>(string name) {
            if (name == "CurrentSite") {
                var siteSettings = _siteService.GetSiteSettings();
                return ctx => (T)siteSettings;
            }
            return null;
        }
    }
}
