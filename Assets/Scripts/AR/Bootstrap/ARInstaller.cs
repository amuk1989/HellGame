using AR.Interfaces;
using AR.Services;
using Utility;
using Zenject;

namespace AR.Bootstrap
{
    public class ARInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IARService>()
                .To<ARService>()
                .AsSingle()
                .NonLazy();
        }
    }
}