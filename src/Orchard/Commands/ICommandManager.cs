using System.Collections.Generic;

namespace Orchard.Commands {
    /// <summary>
    /// ��������ӿ�
    /// </summary>
    public interface ICommandManager : IDependency {
        /// <summary>
        /// ִ��
        /// </summary>
        /// <param name="parameters">��������</param>
        void Execute(CommandParameters parameters);
        /// <summary>
        /// ��ȡ���������б�
        /// </summary>
        /// <returns>������������</returns>
        IEnumerable<CommandDescriptor> GetCommandDescriptors();
    }
}