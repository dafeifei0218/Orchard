namespace Orchard.Caching {
    /// <summary>
    /// �ӷ����ƽӿ�
    /// </summary>
    public interface IVolatileToken {
        /// <summary>
        /// �Ƿ��ǵ�ǰ�Ķ���false������ʧЧ��ture��������Ч
        /// </summary>
        bool IsCurrent { get; }
    }
}