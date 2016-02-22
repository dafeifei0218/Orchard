using System.Collections.Generic;

namespace Orchard.Collections {
    /// <summary>
    /// 项目分页接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageOfItems<out T> : IEnumerable<T> {
        /// <summary>
        /// 页码
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 总项目数
        /// </summary>
        int TotalItemCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPageCount { get; }
        /// <summary>
        /// 开始位置
        /// </summary>
        int StartPosition { get; }
        /// <summary>
        /// 结束位置
        /// </summary>
        int EndPosition { get; }
    }
}