using Scanning.Interfaces;
using Scanning.Repositories;
using Scanning.Services;
using Zenject;

namespace Scanning.Bootstrap
{
    public class ScanningInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<RoomService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<ScanningService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<HolesRepository>()
                .AsSingle()
                .NonLazy();
        }
    }
}