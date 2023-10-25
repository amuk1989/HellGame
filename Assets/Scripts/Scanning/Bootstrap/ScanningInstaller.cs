using Scanning.Services;
using Zenject;

namespace Scanning.Bootstrap
{
    public class ScanningInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ScanningService>()
                .AsSingle()
                .NonLazy();
        }
    }
}