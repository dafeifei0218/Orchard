namespace Orchard.Localization {
    /// <summary>
    /// �յı��ػ���
    /// </summary>
    public static class NullLocalizer {
        /// <summary>
        /// ˽�о�̬���캯��
        /// </summary>
        static NullLocalizer () {
            _instance = (format, args) => new LocalizedString((args == null || args.Length == 0) ? format : string.Format(format, args));
        }
        
        /// <summary>
        /// ��ʼ�����ڲ�����
        /// </summary>
        static readonly Localizer _instance;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public static Localizer Instance { get { return _instance; } }
    }
}