using System.Reflection;

namespace Orchard.Commands {
    /// <summary>
    /// ��������
    /// </summary>
    public class CommandDescriptor {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public MethodInfo MethodInfo { get; set; }
        /// <summary>
        /// �����ı�
        /// </summary>
        public string HelpText { get; set; }
    }
}