namespace Orchard.Caching {
    /// <summary>
    /// �ӷ����ƽӿ�
    /// </summary>
    public interface IVolatileToken {
        /// <summary>
        /// �Ƿ��ǵ�ǰ�Ķ���ture��������Ч��false������ʧЧ
        /// </summary>
        bool IsCurrent { get; }
    }
}