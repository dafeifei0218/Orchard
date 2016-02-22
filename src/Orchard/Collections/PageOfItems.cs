using System;
using System.Collections.Generic;

namespace Orchard.Collections
{
    /// <summary>
    /// 项目分页接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageOfItems<T> : List<T>, IPageOfItems<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">项目</param>
        public PageOfItems(IEnumerable<T> items)
        {
            AddRange(items);
        }

        #region IPageOfItems<T> Members

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总项目数
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount
        {
            get { return (int)Math.Ceiling((double)TotalItemCount / PageSize); }
        }
        /// <summary>
        /// 开始位置
        /// </summary>
        public int StartPosition
        {
            get { return (PageNumber - 1) * PageSize + 1; }
        }
        /// <summary>
        /// 结束位置
        /// </summary>
        public int EndPosition
        {
            get { return PageNumber * PageSize > TotalItemCount ? TotalItemCount : PageNumber * PageSize; }
        }

        #endregion
    }
}