namespace Orchard.ContentManagement.ViewModels {
    /// <summary>
    /// 模板ViewModel
    /// </summary>
    public class TemplateViewModel {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model"></param>
        public TemplateViewModel(object model)
            : this(model, string.Empty) {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model"></param>
        /// <param name="prefix"></param>
        public TemplateViewModel(object model, string prefix) {
            Model = model;
            Prefix = prefix;
        }

        /// <summary>
        /// 模型
        /// </summary>
        public object Model { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZoneName { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool WasUsed { get; set; }
    }
}
