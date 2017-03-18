using Orchard.ContentManagement.FieldStorage;
using Orchard.ContentManagement.MetaData.Models;

namespace Orchard.ContentManagement {
    /// <summary>
    /// 内容字段
    /// </summary>
    public class ContentField {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get { return PartFieldDefinition.Name; } }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get { return PartFieldDefinition.DisplayName; } }

        /// <summary>
        /// 部分字段定义
        /// </summary>
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }
        /// <summary>
        /// 字段定义
        /// </summary>
        public ContentFieldDefinition FieldDefinition { get { return PartFieldDefinition.FieldDefinition; } }

        /// <summary>
        /// 存储
        /// </summary>
        public IFieldStorage Storage { get; set; }
    }
}
