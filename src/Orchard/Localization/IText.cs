namespace Orchard.Localization {
    /// <summary>
    /// 
    /// </summary>
    public interface IText {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textHint"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        LocalizedString Get(string textHint, params object[] args);
    }
}