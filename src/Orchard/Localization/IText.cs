namespace Orchard.Localization {
    /// <summary>
    /// 本地化的文本接口
    /// </summary>
    public interface IText {
        /// <summary>
        /// 获取本地化文本
        /// </summary>
        /// <param name="textHint"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        LocalizedString Get(string textHint, params object[] args);
    }
}