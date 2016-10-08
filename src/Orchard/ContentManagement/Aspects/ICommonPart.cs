using System;
using Orchard.Security;

namespace Orchard.ContentManagement.Aspects {
    /// <summary>
    /// 公共部分内容接口
    /// </summary>
    public interface ICommonPart : IContent {
        /// <summary>
        /// 拥有者
        /// </summary>
        IUser Owner { get; set; }
        /// <summary>
        /// 容器
        /// </summary>
        IContent Container { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreatedUtc { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        DateTime? PublishedUtc { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? ModifiedUtc { get; set; }

        /// <summary>
        /// 版本创建时间
        /// </summary>
        DateTime? VersionCreatedUtc { get; set; }
        /// <summary>
        /// 版本发布时间
        /// </summary>
        DateTime? VersionPublishedUtc { get; set; }
        /// <summary>
        /// 版本修改时间
        /// </summary>
        DateTime? VersionModifiedUtc { get; set; }
        /// <summary>
        /// 版本修改人
        /// </summary>
        string VersionModifiedBy { get; set; }
    }
}
