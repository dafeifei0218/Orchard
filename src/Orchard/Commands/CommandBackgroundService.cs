using Orchard.Tasks;

namespace Orchard.Commands
{
    /// <summary>
    /// Command line specific "no-op" background service implementation.
    /// Note that we make this class "internal" so that it's not auto-registered
    /// by the Orchard Framework (it is registered explicitly by the command
    /// line host).
    /// �������ض��ġ������㡱�ĺ�̨����ʵ�֡�
    /// ��ע�⣬����ʹ����ࡰ�ڲ�����ʹ��������Orchard����Զ�ע�ᣨ������������ʽע�����·��������
    /// </summary>
    internal class CommandBackgroundService : IBackgroundService {
        /// <summary>
        /// 
        /// </summary>
        public void Sweep() {
            // Don't run any background service in command line
            // ���������в������κκ�̨����
        }
    }
}