using System;

namespace Orchard.Caching {
    /// <summary>
    /// ά��ICache�ӿڼ��ϣ��������ڣ��⻧������
    /// </summary>
    public interface ICacheHolder : ISingletonDependency {
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <typeparam name="TKey">��</typeparam>
        /// <typeparam name="TResult">���</typeparam>
        /// <param name="component">���</param>
        /// <returns>����</returns>
        ICache<TKey, TResult> GetCache<TKey, TResult>(Type component);
    }
}
