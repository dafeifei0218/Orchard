namespace Orchard.Localization {
    /// <summary>
    /// 空的本地化类
    /// </summary>
    public static class NullLocalizer {
        /// <summary>
        /// 私有静态构造函数
        /// </summary>
        static NullLocalizer () {
            _instance = (format, args) => new LocalizedString((args == null || args.Length == 0) ? format : string.Format(format, args));
        }
        
        /// <summary>
        /// 初始化的内部变量
        /// </summary>
        static readonly Localizer _instance;

        /// <summary>
        /// 初始化
        /// </summary>
        public static Localizer Instance { get { return _instance; } }
    }
}