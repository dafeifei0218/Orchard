using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Orchard.Data {
    /// <summary>
    /// 仓储借口
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface IRepository<T> {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity">实体</param>
        void Create(T entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(T entity);
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="target">目标</param>
        void Copy(T source, T target);
        /// <summary>
        /// 
        /// </summary>
        void Flush();

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">逐渐</param>
        T Get(int id);
        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="order"></param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="order"></param>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count);
    }
}