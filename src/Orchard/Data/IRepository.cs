using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Orchard.Data {
    /// <summary>
    /// �ִ����
    /// </summary>
    /// <typeparam name="T">����</typeparam>
    public interface IRepository<T> {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void Create(T entity);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void Update(T entity);
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void Delete(T entity);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="source">Դ</param>
        /// <param name="target">Ŀ��</param>
        void Copy(T source, T target);
        /// <summary>
        /// 
        /// </summary>
        void Flush();

        /// <summary>
        /// ����������ȡʵ��
        /// </summary>
        /// <param name="id">��</param>
        T Get(int id);
        /// <summary>
        /// ����������ȡʵ��
        /// </summary>
        /// <param name="predicate">����</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">����</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">����</param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">����</param>
        /// <param name="order"></param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">����</param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count);
    }
}