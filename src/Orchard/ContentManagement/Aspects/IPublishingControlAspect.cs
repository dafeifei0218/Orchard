using System;
using Orchard.ContentManagement.Utilities;

namespace Orchard.ContentManagement.Aspects {
    /// <summary>
    /// 发布控制切面接口
    /// </summary>
    public interface IPublishingControlAspect {
        /// <summary>
        /// 定期发布UTC
        /// </summary>
        LazyField<DateTime?> ScheduledPublishUtc { get; }
    }
}