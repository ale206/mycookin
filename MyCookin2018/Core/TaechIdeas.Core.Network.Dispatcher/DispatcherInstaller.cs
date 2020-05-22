using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace TaechIdeas.Core.Network.Dispatcher
{
    [RunInstaller(true)]
    public partial class DispatcherInstaller : Installer
    {
        public DispatcherInstaller()
        {
            InitializeComponent();
        }

        public DispatcherInstaller(ServiceInstaller serviceInstaller)
        {
            InitializeComponent();
        }
    }
}